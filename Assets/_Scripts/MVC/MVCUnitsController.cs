using System;
using UnityEngine;

namespace _Scripts.MVC
{
    public class MVCUnitsController : MonoBehaviour
    {
        [SerializeField]
        private MVCModelBase _modelBase;
        [SerializeField]
        private StatusBar _statusBar;

        private void Update()
        {
            _statusBar.Refresh(_modelBase.health, _modelBase.maxHealth, false);
            _statusBar.transform.position =  Camera.main.WorldToScreenPoint(_modelBase.position) + _modelBase.offset;
        }
    }
}