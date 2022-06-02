using _Scripts.MVC;
using UnityEngine;

namespace _Scripts.Managers.UnitTypes
{
    public class Turret : StaticMVCReceiverAttackUnit
    {
        public StatusBar StatusBar;

        [SerializeField]
        private MeshRenderer[] _meshRenderers;

        [SerializeField]
        private ParticleSystem _explodeParticle;

        [SerializeField]
        private Material _destroyMaterial;

        protected override void Attack()
        {
            base.Attack();
            OnAnimationShoot();
        }

        protected override void Death()
        {
            base.Death();
            
            foreach (var meshRenderer in _meshRenderers)
            {
                meshRenderer.material = _destroyMaterial;
            }
            
            _explodeParticle.gameObject.SetActive(true);
            _explodeParticle.Play();

            StatusBar.gameObject.SetActive(false);
        }

        public override void Link(StatusBar statusBar)
        {
            StatusBar = statusBar;
        }
    }
}