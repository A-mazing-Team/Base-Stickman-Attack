using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Levels;
using _Scripts.Managers;
using _Scripts.Managers.UnitTypes;
using _Scripts.MVC;
using _Scripts.PlayerBase;
using _Scripts.Save;
using _Scripts.UI;
using _Scripts.Upgrades;
using DefaultNamespace;
using ModestTree;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace _Scripts.Battle
{
    public class BattleManager : MonoBehaviour
    {
        [SerializeField]
        private WinScreen _winUI;
        [SerializeField]
        private GameObject _looseUI;

        [SerializeField]
        private Base _base;

        [SerializeField]
        private LevelData[] _levels;

        [SerializeField]
        private StatusValueBar _statusValueBar;

        [SerializeField]
        private UnitBase[] _staticEnemiesUnits;

        [SerializeField]
        private UnitsScrollController _unitsScroll;

        [SerializeField]
        private StatusBar[] _statusBars;

        [SerializeField]
        private TextMeshProUGUI _levelLabel;

        [SerializeField]
        private TextMeshProUGUI _looseBalanceLabel;


        private List<UnitBase> _allies;

        private List<UnitBase> _enemies;
        private int _avaliableAllyUnitsCount = 0;
        private int _instanceUnitsCounter;

        private bool _isBattleProcess;

        private bool _isBattleStarted = false;

        [HideInInspector]
        public float rewardMultiplier;

        [Inject]
        private TutorialController _tutorialController;

        public int ResultReward
        {
            get
            {
                return (int) (_levels[User.Level].goldReward * Math.Pow(rewardMultiplier, User.GetUpgradeLevel(UpgradeType.Income)));
            }
        }
        public bool CanSpawnAlly
        {
            get
            {
                return _instanceUnitsCounter < _avaliableAllyUnitsCount;
            }
        }

        public int Balance
        {
            get
            {
                return _avaliableAllyUnitsCount - _instanceUnitsCounter;
            }
        }

        public LevelData CurrentLevel
        {
            get
            {
                return _levels[User.Level];
            }
        }

        public bool HasAlly
        {
            get
            {
                return !_allies.IsEmpty();
            }
        }

        private void Awake()
        {
            _allies = new List<UnitBase>();
        }

        private void Start()
        {
            _levelLabel.text = (User.Level +1).ToString();
            
            User.UpdateBattleDeck(_levels[User.Level].deckUnitNames);
            
            _isBattleProcess = true;
            CreateEnemies();
            _avaliableAllyUnitsCount = _levels[User.Level].allyUnitsCount;
            _statusValueBar.Refresh(_instanceUnitsCounter, _avaliableAllyUnitsCount, true);
            StartCoroutine(PassiveAddUnit());

            if (_levels[User.Level].needTutor)
            {
                _tutorialController.CameraMoveStage(_base.transform);
            }
        }

        private void Update()
        {
            if (_isBattleProcess)
            {
                CheckLoose();
            }
        }

        private void CreateEnemies()
        {

            _enemies = new List<UnitBase>();

            var enemiesParent = Instantiate(_levels[User.Level].enemiesPrefab, Vector3.zero, Quaternion.identity);

            foreach (Transform enemy in enemiesParent.transform)
            {
                UnitBase unitBase = enemy.gameObject.GetComponent<UnitBase>();

                if (unitBase == null)
                {
                    unitBase = enemy.GetComponentInChildren<UnitBase>();

                    if (unitBase == null)
                    {
                        continue;
                    }
                }


                _enemies.Add(unitBase);
                unitBase.Create(this);
                unitBase.IsMyTeam = false;
                
                if (unitBase is Turret)
                {
                    var bar = _statusBars.FirstOrDefault(i => i.IsFree);
                    bar?.Link(unitBase);
                    unitBase.Link(bar);
                }
            }

            foreach (var staticEnemies in _staticEnemiesUnits)
            {
                _enemies.Add(staticEnemies);
                staticEnemies.Create(this);
                staticEnemies.IsMyTeam = false;
            }
            
            _base.Create(this);
        }

        private void CheckLoose()
        {
            if (_enemies.IsEmpty() && _base.IsActive == false)
            {
                EndGame(true);
            }

            if (_allies.IsEmpty() && _instanceUnitsCounter >= _avaliableAllyUnitsCount)
            {
                EndGame(false);
            }
        }

        public void AddAlly(UnitBase unit)
        {
            if (!_isBattleStarted)
            {
                _isBattleStarted = true;
            }
            
            unit.IsMyTeam = true;
            _allies.Add(unit);
            _instanceUnitsCounter+= unit.config.cost;
            _statusValueBar.Refresh(_instanceUnitsCounter, _avaliableAllyUnitsCount, true);
            
            if (_levels[User.Level].needTutor)
            {
                if (_tutorialController.currentStage == TutorStage.Spawn)
                {
                    _tutorialController.FinishStage(TutorStage.Spawn);
                }
            }
        }

        public List<UnitBase> GetUnits(bool isMyTeam)
        {
            if (!isMyTeam && _enemies.IsEmpty() && _base.IsActive == true)
            {
                _enemies.Add(_base);
            }
            
            return isMyTeam ? _allies : _enemies;
        }

        public void OnUnitDeath(UnitBase unitBase)
        {
            var l = unitBase.IsMyTeam ? _allies : _enemies;

            foreach (var unit in l)
            {
                if (unit == unitBase)
                {
                    l.Remove(unit);
                    break;
                }
            }
        } 
        private void EndGame(bool state)
        {
            _isBattleProcess = false;
            
            if (state)
            {
                StartCoroutine(EndRoutine(User.Level));
                

                User.Balance += ResultReward;
                
                if (_levels.Length - 1 != User.Level)
                {
                    User.Level++;
                }

                User.SetUnitToNextLevel(_base.config);
            }
            else
            {
                User.Balance += ResultReward / 2;
                _looseBalanceLabel.text = (ResultReward / 2).ToString();
                _looseUI.SetActive(true);
            }
        }

        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }


        private IEnumerator EndRoutine(int prevLevel)
        {
            yield return new WaitForSeconds(3.5f);
            
            
            int result = (int) (_levels[prevLevel].goldReward * Math.Pow(rewardMultiplier, User.GetUpgradeLevel(UpgradeType.Income)));
            
            _winUI.Show(_levels[prevLevel].unlockUnit, _levels[prevLevel].lockPercent, result);
        }

        private IEnumerator PassiveAddUnit()
        {
            while (true)
            {
                if (!_isBattleStarted)
                {
                    yield return null;
                }

                yield return new WaitForSeconds(5f);
                
                if (_instanceUnitsCounter < _avaliableAllyUnitsCount)
                {
                    _instanceUnitsCounter--;
                }

                _statusValueBar.Refresh( _instanceUnitsCounter, _avaliableAllyUnitsCount, true);
                
                yield return null;
            }
            
        }
    }
}