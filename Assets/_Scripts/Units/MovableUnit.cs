using Units;
using UnityEngine;
using UnityEngine.AI;

namespace _Scripts.Managers
{
    public abstract class MovableUnit : UnitBase
    {
        private const float SpeedRotation = 5f;
        
        [SerializeField]
        private NavMeshAgent _navMeshAgent;
        [SerializeField]
        protected UnitAnimator _animator;


        protected void MoveToUnit(UnitBase unitBase)
        {
            if (this is StaticMVCReceiverAttackUnit)
            {
                return;
            }
            
            _navMeshAgent.SetDestination(unitBase.position);

            if (unitBase != this)
            {
                _animator.Move(true);
            }
            else
            {
                _animator.Move(false);
            }
        }

        protected void Rotate(Vector3 to)
        {
            Vector3 dir = (to - position).normalized;
            dir.y = 0f;
            
            Quaternion rotation = Quaternion.LookRotation(dir, Vector3.up);
            
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * SpeedRotation);
        }
    }
}