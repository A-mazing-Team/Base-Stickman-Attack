using _Scripts.Upgrades;
using QFSW.MOP2;
using UnityEngine;

namespace _Scripts.Managers
{
    [CreateAssetMenu]
    public class UnitConfig : ScriptableObject
    {
        public string name;
        public Sprite image;
        public int cost;
        public ObjectPool unitPool;
        
        public float speed;
        public float attackRange;
        public float attackDelay;
        public ObjectPool bulletPool;


        [Header("UpgradeLevels")]
        public UnitUpgrade[] upgrades;
    }
}