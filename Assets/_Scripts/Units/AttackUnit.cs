using System;
using ModestTree;
using UnityEngine;

namespace _Scripts.Managers
{
    public abstract class AttackUnit : MovableUnit
    {
        [SerializeField]
        protected ParticleSystem _shootFx;
        [SerializeField]
        private Bullet _bulletPrefab;
        
        [SerializeField]
        private Transform _shootPosition;

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
            if (!(this is StaticMVCReceiverAttackUnit))
            {
                _animator.Shoot();   
            }
        }

        public void OnAnimationShoot()
        {
            _shootFx.Play();
            
            if (_currentTarget != null && _currentTarget.TakeDamage(config.damage))
            {
                SetTarget();
            }
            
            SpawnBullet();
        }

        private void SpawnBullet()
        {
            if (_currentTarget == null)
            {
                return;
            }
            
            var bullet = Instantiate(_bulletPrefab, _shootPosition.position, Quaternion.Euler(-90,0,0));
            bullet.Shoot(_currentTarget.transform);
        }
    }
    
}