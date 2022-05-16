using System;
using System.Linq;
using _Scripts.Levels;
using UnityEngine;
using UnityEngine.AI;

namespace _Scripts.Managers
{
    public abstract class UnitBase : MonoBehaviour
    {
        public Vector3 position => transform.position;
        public UnitConfig config;

        [SerializeField]
        protected Animator _animator;

        protected LevelData _levelData;
        [SerializeField]
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

        public void Create(UnitConfig config)
        {
            _health = config.health;
        }

        private void Death()
        {
            
        }
        
    }
}