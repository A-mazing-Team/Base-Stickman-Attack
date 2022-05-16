using UnityEngine;

namespace _Scripts.Managers
{
    [CreateAssetMenu]
    public class UnitConfig : ScriptableObject
    {
        public float cost;
        public float health;
        public float damage;
        public float speed;
        public float attackRange;
        public float attackDelay;
    }
}