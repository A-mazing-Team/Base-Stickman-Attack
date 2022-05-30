using System;
using System.Collections.Generic;
using _Scripts.Levels;
using _Scripts.Managers;
using _Scripts.MVC;
using _Scripts.PlayerBase;
using _Scripts.Save;
using _Scripts.UI;
using ModestTree;
using UnityEngine;
using UnityEngine.SceneManagement;

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


        private List<UnitBase> _allies;

        private List<UnitBase> _enemies;
        private int _avaliableAllyUnitsCount = 0;
        private int _instanceUnitsCounter;

        private bool _isBattleProcess;

        public bool CanSpawnAlly
        {
            get
            {
                return _instanceUnitsCounter < _avaliableAllyUnitsCount;
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
            _isBattleProcess = true;
            CreateEnemies();
            _avaliableAllyUnitsCount = _levels[User.Level].allyUnitsCount;
            _statusValueBar.Refresh(_instanceUnitsCounter, _avaliableAllyUnitsCount, true);
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
                    continue;
                }
                
                _enemies.Add(unitBase);
                unitBase.Create(this);
                unitBase.IsMyTeam = false;
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
            if (_enemies.IsEmpty() && _base.gameObject.activeSelf == false)
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
            unit.IsMyTeam = true;
            _allies.Add(unit);
            _instanceUnitsCounter++;
            _statusValueBar.Refresh(_instanceUnitsCounter, _avaliableAllyUnitsCount, true);
        }

        public List<UnitBase> GetUnits(bool isMyTeam)
        {
            if (!isMyTeam && _enemies.IsEmpty() && _base.gameObject.activeSelf == true)
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
                _winUI.Show(_levels[User.Level].unlockUnit);
                User.Level++;
            }
            else
            {
                _looseUI.SetActive(true);
            }
        }

        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}