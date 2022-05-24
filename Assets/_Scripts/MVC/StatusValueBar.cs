using TMPro;
using UnityEngine;

namespace _Scripts.MVC
{
    public class StatusValueBar : StatusBar
    {
        [SerializeField]
        private TextMeshProUGUI _valueLabel;
        public override void Refresh(float value, float maxValue, bool isDiffrent)
        {
            base.Refresh(value, maxValue, isDiffrent);
            
            if (isDiffrent)
            {
                _valueLabel.text = (maxValue - value).ToString();
            }
            else
            {
                _valueLabel.text = value.ToString();
            }
            
        }
    }
}