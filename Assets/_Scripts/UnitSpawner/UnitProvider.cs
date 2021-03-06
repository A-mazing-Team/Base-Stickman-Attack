using System;
using _Scripts.Battle;
using _Scripts.Draw;
using _Scripts.Managers;
using _Scripts.Managers.UnitTypes;
using _Scripts.MVC;
using QFSW.MOP2;
using UnityEngine;
using Zenject;

namespace _Scripts.UnitSpawner
{
    public class UnitProvider : MonoBehaviour
    {
        [SerializeField]
        private Brush _brush;


        [Inject]
        private BattleManager _battleManager;

        private UnitBase _currentSpawnUnit = null;

        private bool _canSpawn = false;
        

        private void OnEnable()
        {
            _brush.OnDeltaPassed += Brush_OnOnDeltaPassed;
        }

        private void OnDisable()
        {
            _brush.OnDeltaPassed -= Brush_OnOnDeltaPassed;
        }

        private void Brush_OnOnDeltaPassed(Vector3 position)
        {
            if (_battleManager.CanSpawnAlly)
            {
                Spawn(position);
            }
        }

        private void Spawn(Vector3 position)
        {
            if (_currentSpawnUnit == null)
            {
                return;
            }
            if (_currentSpawnUnit.config.cost > _battleManager.Balance || !_canSpawn)
            {
                return;
            }
            
            var u = _currentSpawnUnit.config.unitPool.GetObjectComponent<UnitBase>(position, Quaternion.identity);
            u.IsMyTeam = true;
            u.Create(_battleManager);
            _battleManager.AddAlly(u);
        }

       
        public void SpawnedUnitChanged(UnitBase unitBase)
        {
            _currentSpawnUnit = unitBase;

            if (_currentSpawnUnit.GetType() == typeof(Tank) || _currentSpawnUnit.GetType() == typeof(Humvee))
            {
                _brush.spawnOffset = 1.5f;
            }
            else
            {
                _brush.spawnOffset = 0.5f;
            }
        }

        public void CanSpan(bool state)
        {
            _canSpawn = state;
        }
    }
}