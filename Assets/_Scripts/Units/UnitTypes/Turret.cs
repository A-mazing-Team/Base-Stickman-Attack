using _Scripts.MVC;
using UnityEngine;

namespace _Scripts.Managers.UnitTypes
{
    public class Turret : StaticMVCReceiverAttackUnit
    {
        protected override void Attack()
        {
            base.Attack();
            OnAnimationShoot();
        }
    }
}