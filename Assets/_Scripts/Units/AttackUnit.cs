using System;
using UnityEngine;

namespace _Scripts.Managers
{
    public abstract class AttackUnit : MovableUnit
    {
        [SerializeField]
        protected ParticleSystem _shootFx;

        private float _nextShootTime;

        private void Update()
        {
            Validate();
        }

        private void Validate()
        {
            if (Vector3.Distance(_currentTarget.position, position) > config.attackRange)
            {
                MoveToUnit(_currentTarget);
            }
            else
            {
                MoveToUnit(this);
                
                if (Time.time >= _nextShootTime)
                {
                    Attack();
                    _nextShootTime = Time.time + config.attackDelay;
                }
            }
        }

        protected void SetTarget()
        {
            UnitBase nearUnit = _levelData.enemies[0];
            float minDistance = Vector3.Distance(nearUnit.position, position);
            
            for (int i = 0; i < _levelData.enemies.Count; i++)
            {
                var d = Vector3.Distance(_levelData.enemies[i].position, position);
                if (d < minDistance)
                {
                    minDistance = d;
                    nearUnit = _levelData.enemies[i];
                }
            }

            _currentTarget = nearUnit;
        }

        protected virtual void Attack()
        {
            if (_currentTarget.TakeDamage(config.damage))
            {
                SetTarget();
            }
        }
    }
}