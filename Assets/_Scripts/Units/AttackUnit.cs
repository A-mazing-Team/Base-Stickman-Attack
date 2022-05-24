using System;
using ModestTree;
using UnityEngine;

namespace _Scripts.Managers
{
    public abstract class AttackUnit : MovableUnit
    {
        [SerializeField]
        protected ParticleSystem _shootFx;

        private float _nextShootTime;

        protected virtual void Update()
        {
            SetTarget();
            Validate();
        }

        private void Validate()
        {
            
            if (_currentTarget == null)
            {
                return;
            }
            
            Rotate(_currentTarget.position);
            
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
            var units = _battleManager.GetUnits(!IsMyTeam);

            if (units.IsEmpty())
            {
                return;
            }
            
            UnitBase nearUnit = units[0];
            float minDistance = Vector3.Distance(nearUnit.position, position);
            
            for (int i = 0; i < units.Count; i++)
            {
                var d = Vector3.Distance(units[i].position, position);
                if (d < minDistance)
                {
                    minDistance = d;
                    nearUnit = units[i];
                }
            }

            _currentTarget = nearUnit;
            _currentTarget.OnDeath += CurrentTargetOnOnDeath;
        }

        private void CurrentTargetOnOnDeath()
        {
            _currentTarget = null;
        }

        protected virtual void Attack()
        {
            if (_animator == null)
            {
                _shootFx.Play();
            }
            
            
            if (_currentTarget.TakeDamage(config.damage))
            {
                SetTarget();
            }

            if (!(this is StaticMVCReceiverAttackUnit))
            {
                _animator.Shoot();   
            }
        }

        public void Particle()
        {
            _shootFx.Play();
        }
    }
}