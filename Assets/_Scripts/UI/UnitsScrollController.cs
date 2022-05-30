using System;
using _Scripts.Save;
using UnityEngine;

namespace _Scripts.UI
{
    public class UnitsScrollController : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] _unitsCards;
        
        private void Start()
        {
            int counter = 0;
            
            for (; counter <= User.Level; counter++)
            {
                _unitsCards[counter].SetActive(true);
            }

            for (; counter < _unitsCards.Length; counter++)
            {
                _unitsCards[counter].SetActive(false);
            }
        }
    }
}