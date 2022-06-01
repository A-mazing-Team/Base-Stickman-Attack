using System;
using _Scripts.Save;
using TMPro;
using UnityEngine;

namespace _Scripts.UI
{
    public class UIBalance : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _textMeshProUGUI;


        private void Start()
        {
            Refresh();
        }

        private void OnEnable()
        {
            User.BalanceChanged += Refresh;
        }

        private void OnDisable()
        {
            User.BalanceChanged -= Refresh;
        }

        private void Refresh()
        {
            _textMeshProUGUI.text = User.Balance.ToString();
        }
    }
}