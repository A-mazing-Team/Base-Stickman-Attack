using System;
using _Scripts.Managers;
using _Scripts.Save;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.UI.Collection
{
    public class CollectionUnitCard : MonoBehaviour
    {
        [SerializeField]
        private Image _image;
        [SerializeField]
        private TextMeshProUGUI _levelLabel;
        [SerializeField]
        private Button _upgradeButton;
        [SerializeField]
        private Button _selectButton;
        [SerializeField]
        private Outline _outline;

        public bool isInDeck;
        public UnitConfig linkedConfig;

        public void Refresh(UnitConfig config, Action<CollectionUnitCard> selectAction)
        {
            RemoveButtonsListeners();
            
            _image.sprite = config.image;
            _levelLabel.text = User.GetUnitLevel(config).ToString();
            linkedConfig = config;
            
            _selectButton.onClick.AddListener((() =>
            {
                selectAction.Invoke(this);
            }));
        }

        private void RemoveButtonsListeners()
        {
            _selectButton.onClick.RemoveAllListeners();
            _upgradeButton.onClick.RemoveAllListeners();
        }

        public void SetState(bool on)
        {
            _outline.enabled = on;
        }
    }
}