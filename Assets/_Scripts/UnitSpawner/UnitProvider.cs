using System;
using _Scripts.Draw;
using _Scripts.Managers;
using UnityEngine;

namespace _Scripts.UnitSpawner
{
    public class UnitProvider : MonoBehaviour
    {
        [SerializeField]
        private Brush _brush;
        
        [SerializeField]
        private UnitBase _currentSpawnUnit;

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
            Instantiate(_currentSpawnUnit, position, Quaternion.identity);
        }
    }
}