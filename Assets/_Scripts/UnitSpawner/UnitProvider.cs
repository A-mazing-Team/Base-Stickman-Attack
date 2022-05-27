using System;
using _Scripts.Battle;
using _Scripts.Draw;
using _Scripts.Managers;
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
        private UnitBase _sniper;
        [SerializeField]
        private UnitBase _shooter;
        [SerializeField]
        private UnitBase _rocketLauncher;
        
        private UnitBase _currentSpawnUnit;

        [Inject]
        private BattleManager _battleManager;
        

        private void OnEnable()
        {
            _brush.OnDeltaPassed += Brush_OnOnDeltaPassed;
        }

        private void OnDisable()
        {
            _brush.OnDeltaPassed -= Brush_OnOnDeltaPassed;
        }

        private void SetSpawnUnit(UnitBase unitBase)
        {
            _currentSpawnUnit = unitBase;
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
            var u = Instantiate(_currentSpawnUnit, position, Quaternion.identity);
            u.Create(_battleManager);
            _battleManager.AddAlly(u);
        }

        public void Sniper()
        {
            _currentSpawnUnit = _sniper;
        }
        public void Shooter()
        {
            _currentSpawnUnit = _shooter;
        }

        public void RocketLauncher()
        {
            _currentSpawnUnit = _rocketLauncher;
        }
    }
}