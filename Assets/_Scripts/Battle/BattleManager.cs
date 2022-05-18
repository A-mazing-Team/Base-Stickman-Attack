using System;
using System.Collections.Generic;
using _Scripts.Managers;
using _Scripts.PlayerBase;
using ModestTree;
using UnityEngine;

namespace _Scripts.Battle
{
    public class BattleManager : MonoBehaviour
    {
        [SerializeField]
        private List<UnitBase> _enemies;
        [SerializeField]
        private Base _base;
        
        private List<UnitBase> _allies;

        private void Awake()
        {
            _allies = new List<UnitBase>();
        }

        private void Start()
        {
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
    }
}