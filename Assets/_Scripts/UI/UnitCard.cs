using System;
using _Scripts.Battle;
using _Scripts.Managers;
using _Scripts.UnitSpawner;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Scripts.UI
{
    public class UnitCard : MonoBehaviour
    {
        [SerializeField]
        private UnitBase _unitBase;
        [SerializeField]
        private Button _button;
        [SerializeField]
        private Outline _outline;

        public bool isActive;

        [Inject]
        private UnitProvider _unitProvider;

        public void Init(Action<UnitCard> callback)
        {
            _button.onClick.AddListener((() =>
            {
                ChangeState(true);
                _unitProvider.SpawnedUnitChanged(_unitBase);
                callback?.Invoke(this);
            }));
        }

        public void ChangeState(bool on)
        {
            isActive = on;
            _outline.enabled = on;
        }
    }
}