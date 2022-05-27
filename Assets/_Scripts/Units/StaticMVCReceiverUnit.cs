using _Scripts.MVC;
using UnityEngine;

namespace _Scripts.Managers
{
    public class StaticMVCReceiverUnit : UnitBase
    {
        [SerializeField]
        protected MVCModelBase _modelBase;
        
        private void Update()
        {
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