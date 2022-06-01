using _Scripts.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.UI
{
    public class WinScreen : MonoBehaviour
    {
        [SerializeField]
        private Image _unlockImage;
        [SerializeField]
        private Image _lockImage;
        
        [SerializeField]
        private GameObject _completeGroup;
        [SerializeField]
        private TextMeshProUGUI _unlockPercentLabel;
        
        public void Show(UnitConfig unlockUnit, int unlockPercent)
        {
            gameObject.SetActive(true);
            _completeGroup.SetActive(unlockUnit != null);
            _unlockImage.sprite = unlockUnit.image;
            _lockImage.sprite = unlockUnit.image;
            _lockImage.fillAmount = unlockPercent * 0.01f;
            
            _unlockPercentLabel.text = (100 - unlockPercent).ToString() + "%";
        }
    }
}