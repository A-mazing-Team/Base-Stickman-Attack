using System;
using _Scripts.Managers;
using _Scripts.Save;
using UnityEngine;
using Zenject;

namespace _Scripts.UI.Collection
{
    public class BattleDeck: MonoBehaviour
    {
        [SerializeField]
        private CollectionUnitCard[] _battleDeckCards;

        [SerializeField]
        private CollectionUnitCard[] _collectionCards;

        [Inject]
        private UnitService _unitService;

        private CollectionUnitCard _currentDeckSelected = null;
        private CollectionUnitCard _currentCollectionSelected = null;

        private void OnEnable()
        {
            var data = User.GetBattleDeckUnitNames();
            
            for (int i = 0; i < _battleDeckCards.Length; i++)
            {
                _battleDeckCards[i].Refresh(_unitService.GetUnitByName(data[i]), OnCardClicked);
                _battleDeckCards[i].isInDeck = true;
            }
        }

        private void OnCardClicked(CollectionUnitCard card)
        {
            if (card == _currentDeckSelected && card == _currentCollectionSelected)
            {
                return;
            }

            if (card.isInDeck)
            {
                _currentDeckSelected.SetState(false);
                _currentDeckSelected = card;
                _currentDeckSelected.SetState(true); 
            }
            else
            {
                _currentCollectionSelected.SetState(false);
                _currentCollectionSelected = card;
                _currentCollectionSelected.SetState(true); 
            }

            if (_currentCollectionSelected!= null && _currentDeckSelected !=null)
            {
                Swap();
            }
            
        }
        
        private void Swap()
        {
            var tempConfig = _currentCollectionSelected.linkedConfig;
            
            _currentCollectionSelected.Refresh(_currentDeckSelected.linkedConfig, OnCardClicked);
            _currentDeckSelected.Refresh(tempConfig, OnCardClicked);
            
            _currentCollectionSelected.SetState(false);
            _currentDeckSelected.SetState(false);

            _currentCollectionSelected = null;
            _currentDeckSelected = null;
            
            //User.UpdateBattleDeck();
        }
    }
}