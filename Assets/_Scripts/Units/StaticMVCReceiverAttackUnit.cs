using _Scripts.MVC;
using _Scripts.Save;
using _Scripts.Upgrades;
using UnityEngine;

namespace _Scripts.Managers
{
    public class StaticMVCReceiverAttackUnit : AttackUnit
    {
        [SerializeField]
        protected MVCModelBase _modelBase;
        
        protected override void Update()
        {
            base.Update();

            if (_modelBase == null)
            {
                return;
            }
            
            _modelBase.health = _health;
            _modelBase.position = transform.position;
        }

        protected override void InitAdditionalData()
        {
            if (_modelBase == null)
            {
                return;
            }
            
            _modelBase.maxHealth = config.health * config.healthMultiplier * User.GetUpgradeLevel(UpgradeType.Health);
        }

        protected override void Death()
        {
            
            base.Death();
            
            if (_modelBase == null)
            {
                return;
            }
            _modelBase.health = 0;
        }
    }
}