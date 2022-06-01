using System;
using _Scripts.Save;
using _Scripts.Upgrades;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.UI
{
    public class UpgradeCard : MonoBehaviour
    {
        [SerializeField]
        private UpgradeType _upgradeType;
        [SerializeField]
        private Button _button;
        [SerializeField]
        private TextMeshProUGUI _levelValueLabel;
        [SerializeField]
        private TextMeshProUGUI _costLabel;

        [SerializeField]
        private int _baseCost;
        [SerializeField]
        private float _multiplier;

        private float Cost => _baseCost * (User.GetUpgradeLevel(_upgradeType) + 1) * _multiplier;
        private bool CanUpgrade => Cost < User.Balance;

        private void Start()
        {
            Refresh();
        }

        public void Refresh()
        {
            _costLabel.color = CanUpgrade ? Color.white : Color.red;
            _costLabel.text = ((int)Cost).ToString();
            
            _levelValueLabel.text = User.GetUpgradeLevel(_upgradeType).ToString();
            
            _button.onClick.AddListener(Upgrade);
        }

        private void Upgrade()
        {
            if (User.Balance < Cost)
            {
                return;
            }

            User.Balance -= (int)Cost;
            User.AddUpgradeLevel(_upgradeType);
            
            Refresh();
        }

    }
}