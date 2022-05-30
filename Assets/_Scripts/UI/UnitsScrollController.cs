using System;
using _Scripts.Save;
using UnityEngine;

namespace _Scripts.UI
{
    public class UnitsScrollController : MonoBehaviour
    {
        [SerializeField]
        private UnitCard[] _unitsCards;

        private UnitCard _curentActive;
        
        private void Start()
        {
            int counter = 0;
            
            for (; counter <= User.Level; counter++)
            {
                _unitsCards[counter].gameObject.SetActive(true);

                _unitsCards[counter].Init(card =>
                {
                    CardClicked(card);
                });
            }

            for (; counter < _unitsCards.Length; counter++)
            {
                _unitsCards[counter].gameObject.SetActive(false);
            }
            
        }

        private void CardClicked(UnitCard card)
        {
            if (_curentActive == card)
            {
                return;
            }
            
            _curentActive?.ChangeState(false);

            foreach (var unitCard in _unitsCards)
            {
                if (card == unitCard)
                {
                    _curentActive = card;
                    _curentActive.ChangeState(true);
                    break;
                }
            }
        }
    }
}