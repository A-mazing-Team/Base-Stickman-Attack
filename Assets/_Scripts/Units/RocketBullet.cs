using UnityEngine;

namespace _Scripts.Managers
{
    public class RocketBullet: Bullet
    {
        [SerializeField]
        private TrailRenderer _trailRenderer;

        protected override void Reset()
        {
            base.Reset();
            _trailRenderer.Clear();
        }
    }
}