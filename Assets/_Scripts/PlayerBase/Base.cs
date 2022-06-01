using System;
using _Scripts.Managers;
using _Scripts.MVC;
using UnityEngine;

namespace _Scripts.PlayerBase
{
    public class Base : StaticMVCReceiverUnit
    {
        [SerializeField]
        private ParticleSystem _explosionParticle;
        
        protected override void Death()
        {
            base.Death();
            _explosionParticle.Play();
        }
    }
}