using System;
using System.Linq;
using _Scripts.Battle;
using _Scripts.Levels;
using _Scripts.MVC;
using _Scripts.Save;
using _Scripts.Upgrades;
using QFSW.MOP2;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using Zenject;

namespace _Scripts.Managers
{
    public abstract class UnitBase : PoolableMonoBehaviour
    {
        public event Action OnDeath = null;
        public Vector3 position => transform.position;
        public UnitConfig config;

        [HideInInspector]
        public bool IsMyTeam;

        protected BattleManager _battleManager;
        
        protected UnitBase _currentTarget;

        protected bool _isPrepare;
        
        protected ObjectPool _bulletPool;
        

        //######STATS#######
        protected float _health;

        public float CurrentHealth => _health;

        private void OnEnable()
        {
            SceneManager.sceneUnloaded += ResetPool;
        }

        private void OnDisable()
        {
            SceneManager.sceneUnloaded -= ResetPool;
        }

        // true ==  target death
        public bool TakeDamage(float damage)
        {
            float newValue = _health - damage;

            if (newValue <= 0)
            {
                Death();
                return true;
            }
            else
            {
                _health = newValue;
                return false;
            }
            
        }

        public void Create(BattleManager battleManager)
        {
            //_health = config.health;
            _battleManager = battleManager;
            _health = config.health * config.healthMultiplier * User.GetUpgradeLevel(UpgradeType.Health);
            _bulletPool = config.bulletPool;
            InitAdditionalData();
            _isPrepare = true;
        }

        protected virtual void Death()
        {
            OnDeath?.Invoke();
            _battleManager.OnUnitDeath(this);
            this.gameObject.SetActive(false);

            ResetPool();
        }

        private void ResetPool()
        {
            if (config.unitPool != null && IsMyTeam)
            {
                Release();
            }
        }
        
        private void ResetPool(Scene scene)
        {
            ResetPool();
        }

        protected virtual void InitAdditionalData()
        {
        }
        
        public virtual void Link(StatusBar statusBar)
        {
        }
    }
}