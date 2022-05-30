using _Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.UI
{
    public class WinScreen : MonoBehaviour
    {
        [SerializeField]
        private Image _unlockImage;
        [SerializeField]
        private GameObject _completeGroup;
        
        public void Show(UnitConfig unlockUnit)
        {
            _completeGroup.SetActive(unlockUnit != null);
            gameObject.SetActive(true);
            _unlockImage.sprite = unlockUnit.image;
        }
    }
}