namespace _Scripts.Managers.UnitTypes
{
    public class LinkedTurret : AttackUnit
    {
        protected override void Attack()
        {
            OnAnimationShoot();
        }

        protected override void MoveToUnit(UnitBase unitBase)
        {
            
        }
    }
}