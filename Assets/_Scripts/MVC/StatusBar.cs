using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.MVC
{
    public class StatusBar : MonoBehaviour
    {
        [SerializeField]
        private Image _image;

        public virtual void Refresh(float value, float maxValue)
        {
            _image.fillAmount = value / maxValue;
        }
    }
}