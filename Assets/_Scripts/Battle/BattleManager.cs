using System;
using System.Collections.Generic;
using _Scripts.Levels;
using _Scripts.Managers;
using _Scripts.MVC;
using _Scripts.PlayerBase;
using ModestTree;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts.Battle
{
    public class BattleManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject _winUI;
        [SerializeField]
        private GameObject _looseUI;
        
        [SerializeField]
        private List<UnitBase> _enemies;
        [SerializeField]
        private Base _base;
        [SerializeField]
        private LevelData[] _levels;
        [SerializeField]
        private StatusValueBar _statusValueBar;
        
        private List<UnitBase> _allies;

        private int _avaliableAllyUnitsCount = 0;
        private int _currentLevel = 0;
        private int _instanceUnitsCounter;

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
            _avaliableAllyUnitsCount = _levels[_currentLevel].allyUnitsCount;
            _statusValueBar.Refresh(_instanceUnitsCounter, _avaliableAllyUnitsCount, true);
            MarkEnemies();
        }

        private void Update()
        {
            CheckLoose();
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

        private void MarkEnemies()
        {
            foreach (var enemy in _enemies)
            {
                enemy.Create(this);
                enemy.IsMyTeam = false;
            }
            
            _base.Create(this);
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
            if (state)
            {
                _winUI.SetActive(true);
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