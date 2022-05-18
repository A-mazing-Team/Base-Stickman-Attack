using System;
using System.Linq;
using _Scripts.Battle;
using _Scripts.Levels;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace _Scripts.Managers
{
    public abstract class UnitBase : MonoBehaviour
    {
        public event Action OnDeath = null;
        public Vector3 position => transform.position;
        public UnitConfig config;

        [HideInInspector]
        public bool IsMyTeam;

        protected BattleManager _battleManager;
        
        protected UnitBase _currentTarget;
        

        //######STATS#######
        private float _health;


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
            _health = config.health;
        }

        private void Death()
        {
            OnDeath?.Invoke();
            _battleManager.OnUnitDeath(this);
            this.gameObject.SetActive(false);
        }
    }
}