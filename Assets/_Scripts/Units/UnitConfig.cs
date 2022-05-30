using _Scripts.Upgrades;
using UnityEngine;

namespace _Scripts.Managers
{
    [CreateAssetMenu]
    public class UnitConfig : ScriptableObject
    {
        public string name;
        public Sprite image;
        public int cost;
        
        public float speed;
        public float attackRange;
        public float attackDelay;

        [Header("UpgradeLevels")]
        public UnitUpgrade[] upgrades;
    }
}