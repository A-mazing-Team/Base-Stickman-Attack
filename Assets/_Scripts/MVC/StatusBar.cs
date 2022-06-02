using System;
using _Scripts.Managers;
using _Scripts.Save;
using _Scripts.Upgrades;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.MVC
{
    public class StatusBar : MonoBehaviour
    {
        [SerializeField]
        private Image _image;
        private UnitBase _unitBase;
        public bool IsFree => _unitBase == null;
        
        private Vector3 _offset = new Vector3(0, 80, 0);
        private float _scaleFactor;

        private void Start()
        {
            _scaleFactor = GetComponentInParent<Canvas>().scaleFactor;
        }

        public virtual void Refresh(float value, float maxValue, bool isDiffrent)
        {
            _image.fillAmount = value / maxValue;
        }

        private void Update()
        {
            if (_unitBase != null)
            {
                float delta = (_unitBase.config.health * _unitBase.config.healthMultiplier) - _unitBase.config.health;
                
                _image.fillAmount = _unitBase.CurrentHealth / ((User.GetUpgradeLevel(UpgradeType.Health) * delta) + _unitBase.config.health);
                transform.position =  Camera.main.WorldToScreenPoint(_unitBase.position) + _offset * _scaleFactor;
            }
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }

        public void Link(UnitBase unitBase)
        {
            gameObject.SetActive(true);
            _unitBase = unitBase;
        }
    }
}