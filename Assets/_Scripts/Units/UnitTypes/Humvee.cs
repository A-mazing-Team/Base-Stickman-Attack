using System;
using UnityEngine;

namespace _Scripts.Managers.UnitTypes
{
    public class Humvee : MovableUnit
    {
        [SerializeField]
        private UnitBase _linkedTurret;
        private void Update()
        {
            if (!_isPrepare)
            {
                return;
            }
            
            SetTarget();
            
            
            if (Vector3.Distance(_currentTarget.position, position) > config.attackRange)
            {
                MoveToUnit(_currentTarget);
            }
            else
            {
                MoveToUnit(this);
            }
        }

        protected override void Death()
        {
            _linkedTurret.TakeDamage(_linkedTurret.config.health + 1);
            base.Death();
        }
    }
}