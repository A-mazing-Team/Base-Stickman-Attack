using _Scripts.MVC;
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
            _modelBase.health = _health;
            _modelBase.position = transform.position;
        }

        protected override void InitAdditionalData()
        {
            _modelBase.maxHealth = config.health;
        }

        protected override void Death()
        {
            base.Death();
            _modelBase.health = 0;
        }
    }
}