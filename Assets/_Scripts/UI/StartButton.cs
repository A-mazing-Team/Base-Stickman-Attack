using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.UI
{
    public class StartButton : MonoBehaviour
    {
        [SerializeField]
        private Button _button;

        [SerializeField]
        private UnitsScrollController _unitsScroll;

        private void Start()
        {
            _button.onClick.AddListener((() =>
            {
                _unitsScroll.gameObject.SetActive(true);
                _unitsScroll.InitializeCards();
            }));
        }
    }
}