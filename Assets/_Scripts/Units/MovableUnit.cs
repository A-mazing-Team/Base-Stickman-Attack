using System;
using System.Linq;
using _Scripts.Managers.UnitTypes;
using ModestTree;
using Units;
using UnityEngine;
using UnityEngine.AI;
using Object = UnityEngine.Object;

namespace _Scripts.Managers
{
    public abstract class MovableUnit : UnitBase
    {
        private const float SpeedRotation = 5f;
        
        [SerializeField]
        private NavMeshAgent _navMeshAgent;
        [SerializeField]
        protected UnitAnimator _animator;

        protected readonly Type[] NotAnimatedTypes = new[]
        {
            typeof(Humvee),
            typeof(LinkedTurret)
        };
            
        protected virtual void MoveToUnit(UnitBase unitBase)
        {
            if (this is StaticMVCReceiverAttackUnit)
            {
                return;
            }
            
            _navMeshAgent.SetDestination(unitBase.position);

            if (NotAnimatedTypes.Any(i => i == unitBase.GetType()))
            {
                return;
            }
            
            if (unitBase != this)
            {
                _animator?.Move(true);
            }
            else
            {
                _animator?.Move(false);
            }
            
            
        }

        protected void Rotate(Vector3 to)
        {
            Vector3 dir = (to - position).normalized;
            dir.y = 0f;
            
            Quaternion rotation = Quaternion.LookRotation(dir, Vector3.up);
            
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * SpeedRotation);
        }
        
        protected virtual void SetTarget()
        {
            var units = _battleManager.GetUnits(!IsMyTeam);

            if (units.IsEmpty())
            {
                return;
            }
            
            UnitBase nearUnit = units[0];
            float minDistance = Vector3.Distance(nearUnit.position, position);
            
            for (int i = 0; i < units.Count; i++)
            {
                var d = Vector3.Distance(units[i].position, position);
                if (d < minDistance)
                {
                    minDistance = d;
                    nearUnit = units[i];
                }
            }

            _currentTarget = nearUnit;
        }
    }
}