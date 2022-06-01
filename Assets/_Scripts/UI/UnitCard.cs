using System;
using _Scripts.Battle;
using _Scripts.Managers;
using _Scripts.Save;
using _Scripts.UnitSpawner;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Scripts.UI
{
    public class UnitCard : MonoBehaviour
    {
        [HideInInspector]
        public UnitBase unitBase;

        [SerializeField]
        private Image _unitIcon;

        [SerializeField]
        private Button _button;

        [SerializeField]
        private Outline _outline;

        [SerializeField]
        private TextMeshProUGUI _upgradeCostLabel;

        [SerializeField]
        private GameObject _group;

        


        public void Refresh(UnitBase unit, Action<UnitCard> callback, bool isBattleRefresh)
        {
            unitBase = unit;
            _unitIcon.sprite = unit.config.image;

            _group.gameObject.SetActive(false);
            _upgradeCostLabel.gameObject.SetActive(false);


            // if (!isBattleRefresh)
            // {
            //     _upgradeCostLabel.text =
            //         unitBase.config.upgrades[User.GetUnitLevel(unitBase.config) + 1].upgradeCost.ToString();
            //     
            //     bool canUpgrade = unitBase.config.upgrades[User.GetUnitLevel(unitBase.config) + 1].upgradeCost < User.Balance;
            //
            //     _upgradeCostLabel.color = canUpgrade ? Color.white : Color.red;
            // }


            _button.onClick.AddListener((() => { callback?.Invoke(this); }));
        }

        public void ChangeState(bool on)
        {
            _outline.enabled = on;

            var rect = GetComponent<RectTransform>();
            
            if (on)
            {
                rect.pivot = new Vector2(0.5f, 0f);
            }
            else
            {
                rect.pivot = new Vector2(0.5f, 0.5f);
            }
        }

        private void Reset()
        {
            
        }
    }
}