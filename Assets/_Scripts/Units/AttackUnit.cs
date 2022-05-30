using System;
using System.Linq;
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
            if (!_isPrepare)
            {
                return;
            }
            
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

        protected override void SetTarget()
        {
            base.SetTarget();
            
            if (_currentTarget != null)
            {
                _currentTarget.OnDeath += CurrentTargetOnOnDeath;
            }
            
        }

        private void CurrentTargetOnOnDeath()
        {
            _currentTarget = null;
        }

        protected virtual void Attack()
        {
            if (!(this is StaticMVCReceiverAttackUnit) || NotAnimatedTypes.All(i => i != this.GetType()))
            {
                _animator?.Shoot();   
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