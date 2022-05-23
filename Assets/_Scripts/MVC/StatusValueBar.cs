using TMPro;
using UnityEngine;

namespace _Scripts.MVC
{
    public class StatusValueBar : StatusBar
    {
        [SerializeField]
        private TextMeshProUGUI _valueLabel;
        public override void Refresh(float value, float maxValue)
        {
            base.Refresh(value, maxValue);
            _valueLabel.text = (maxValue - value).ToString();
        }
    }
}