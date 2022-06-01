using _Scripts.MVC;
using _Scripts.Save;
using _Scripts.Upgrades;
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
            base.InitAdditionalData();
            //_modelBase.maxHealth = config.health * config.healthMultiplier * User.GetUpgradeLevel(UpgradeType.Health);
            _modelBase.maxHealth = _battleManager.CurrentLevel.baseHp;
            _health = _battleManager.CurrentLevel.baseHp;
        }
    }
}