using System;
using _Scripts.Battle;
using _Scripts.Draw;
using _Scripts.Managers;
using _Scripts.Managers.UnitTypes;
using _Scripts.MVC;
using UnityEngine;
using Zenject;

namespace _Scripts.UnitSpawner
{
    public class UnitProvider : MonoBehaviour
    {
        [SerializeField]
        private Brush _brush;
        [SerializeField]
        private Pistol _defaultSpawnedUnit;
        
        private UnitBase _currentSpawnUnit;


        [Inject]
        private BattleManager _battleManager;

        private void Start()
        {
            _currentSpawnUnit = _defaultSpawnedUnit;
        }

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
            if (_currentSpawnUnit.config.cost > _battleManager.Balance)
            {
                return;
            }
            
            var u = Instantiate(_currentSpawnUnit, position, Quaternion.identity);
            u.IsMyTeam = true;
            u.Create(_battleManager);
            _battleManager.AddAlly(u);
        }

       
        public void SpawnedUnitChanged(UnitBase unitBase)
        {
            _currentSpawnUnit = unitBase;
        }
    }
}