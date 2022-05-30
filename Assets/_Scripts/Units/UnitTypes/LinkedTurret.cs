namespace _Scripts.Managers.UnitTypes
{
    public class LinkedTurret : AttackUnit
    {
        protected override void Attack()
        {
            base.Attack();
            OnAnimationShoot();
        }

        protected override void MoveToUnit(UnitBase unitBase)
        {
            
        }
    }
}