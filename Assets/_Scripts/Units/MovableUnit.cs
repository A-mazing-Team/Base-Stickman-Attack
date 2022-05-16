using UnityEngine;
using UnityEngine.AI;

namespace _Scripts.Managers
{
    public abstract class MovableUnit : UnitBase
    {
        [SerializeField]
        private NavMeshAgent _navMeshAgent;
        
        
        protected void MoveToUnit(UnitBase unitBase)
        {
            _navMeshAgent.SetDestination(unitBase.position);

            if (unitBase != this)
            {
                _animator.SetBool("Walk", true);
            }
            else
            {
                _animator.SetBool("Walk", false);
            }
        }
    }
}