using System;
using System.Collections;
using _Scripts.Managers;
using _Scripts.MVC;
using UnityEngine;

namespace _Scripts.PlayerBase
{
    public class Base : StaticMVCReceiverUnit
    {
        [SerializeField]
        private ParticleSystem _explosionParticle;
        [SerializeField]
        private ParticleSystem _confetti;

        [SerializeField]
        private MeshRenderer _meshRenderer;

        [SerializeField]
        private StatusBar _statusBar;

        public bool IsActive { get; set; }


        private void Start()
        {
            IsActive = true;
        }

        protected override void Death()
        {
            _statusBar.Refresh(0, 10, false);
            _statusBar.gameObject.SetActive(false);
            
            _meshRenderer.material = _meshRenderer.materials[1];
            
            IsActive = false;
            base.Death();
            gameObject.SetActive(true);
            
            _explosionParticle.gameObject.SetActive(true);
            _explosionParticle.Play();

            StartCoroutine(Delay());
        }

        private IEnumerator Delay()
        {
            yield return null;
            _modelBase.health = 0;
            yield return new WaitForSeconds(1.5f);
            _confetti.gameObject.SetActive(true);
            _confetti.Play();
        }
    }
}