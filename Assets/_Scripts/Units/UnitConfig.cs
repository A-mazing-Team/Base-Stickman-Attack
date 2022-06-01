using _Scripts.Upgrades;
using QFSW.MOP2;
using UnityEngine;

namespace _Scripts.Managers
{
    [CreateAssetMenu]
    public class UnitConfig : ScriptableObject
    {
        public UnitBase prefab;
        public string name;
        public Sprite image;
        public int cost;
        public ObjectPool unitPool;
        public float speed;
        public float attackRange;
        public float attackDelay;
        public ObjectPool bulletPool;
        

         [Header("Stats")]
         public float health;
         public float healthMultiplier;
         public float damage;
         public float damageMultiplier;
    }
}