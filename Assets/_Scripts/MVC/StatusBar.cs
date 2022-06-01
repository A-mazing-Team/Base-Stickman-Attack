using System;
using _Scripts.Managers;
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
        
        private Vector3 _offset = new Vector3(0, 130, 0);
        
        public virtual void Refresh(float value, float maxValue, bool isDiffrent)
        {
            _image.fillAmount = value / maxValue;
        }

        private void Update()
        {
            if (_unitBase != null)
            {
                _image.fillAmount = _unitBase.CurrentHealth / _unitBase.config.health;
                transform.position =  Camera.main.WorldToScreenPoint(_unitBase.position) + _offset;
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