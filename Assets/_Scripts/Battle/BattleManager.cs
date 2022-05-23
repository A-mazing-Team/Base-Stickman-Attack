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

        private void Awake()
        {
            _allies = new List<UnitBase>();
        }

        private void Start()
        {
            _avaliableAllyUnitsCount = _levels[_currentLevel].allyUnitsCount;
            _statusValueBar.Refresh(_instanceUnitsCounter, _avaliableAllyUnitsCount);
            MarkEnemies();
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
            _statusValueBar.Refresh(_instanceUnitsCounter, _avaliableAllyUnitsCount);
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

            if (l.IsEmpty())
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}