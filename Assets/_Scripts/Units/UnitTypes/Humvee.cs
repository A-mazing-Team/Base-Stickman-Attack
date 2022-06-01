using System;
using _Scripts.Save;
using _Scripts.Upgrades;
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


            if (_currentTarget != null && Vector3.Distance(_currentTarget.position, position) > config.attackRange)
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
            float delta = (config.health * config.healthMultiplier) - config.health;
            _linkedTurret.TakeDamage((int)(delta * User.GetUpgradeLevel(UpgradeType.Health) + 1) + config.health);
            base.Death();
        }

        protected override void InitAdditionalData()
        {
            base.InitAdditionalData();
            _linkedTurret.Create(_battleManager);
            _linkedTurret.IsMyTeam = IsMyTeam;
        }
    }
}