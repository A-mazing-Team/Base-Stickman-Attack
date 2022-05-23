using UnityEngine;

namespace _Scripts.MVC
{
    [CreateAssetMenu]
    public class MVCModelBase : ScriptableObject
    {
        public Vector3 offset;
        [HideInInspector]
        public float maxHealth;
        [HideInInspector]
        public float health;
        [HideInInspector]
        public Vector3 position;
    }
}