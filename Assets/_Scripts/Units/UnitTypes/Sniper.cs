using UnityEngine;

namespace _Scripts.Managers.UnitTypes
{
    public class Sniper : AttackUnit
    {
        protected override void Attack()
        {
            _shootFx.Play();
        }
    }
}