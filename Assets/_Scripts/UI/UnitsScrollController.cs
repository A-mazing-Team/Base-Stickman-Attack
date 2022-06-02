using System;
using _Scripts.Managers;
using _Scripts.Save;
using _Scripts.UnitSpawner;
using DefaultNamespace;
using UnityEngine;
using Zenject;

namespace _Scripts.UI
{
    public class UnitsScrollController : MonoBehaviour
    {
        [SerializeField]
        private UnitCard[] _unitsCards;

        private UnitCard _curentActive;

        [Inject]
        private UnitService _unitService;

        [Inject]
        private UnitProvider _unitProvider;
        
        [Inject]
        private TutorialController _tutorialController;

        public void InitializeCards()
        {
            int counter = 0;

            var data = User.GetBattleDeckUnitNames();


            for (; counter < data.Length; counter++)
            {
                _unitsCards[counter].Refresh(_unitService.GetUnitByName(data[counter]).prefab, CardClickedInBattle,
                    true);
            }


            for (; counter < _unitsCards.Length; counter++)
            {
                _unitsCards[counter].gameObject.SetActive(false);
            }
        }


        private void CardClickedInBattle(UnitCard card)
        {
            if (_tutorialController.currentStage == TutorStage.Unit)
            {
                _tutorialController.FinishStage(TutorStage.Unit);
            }
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
                    _unitProvider.SpawnedUnitChanged(_curentActive.unitBase);
                    break;
                }
            }
        }

        // private void CardClickedInLobby(UnitCard card)
        // {
        //     int upgradeCost = card.unitBase.config.upgrades[User.GetUnitLevel(card.unitBase.config) + 1].upgradeCost;
        //     
        //     bool canUpgrade = upgradeCost < User.Balance;
        //
        //     if (canUpgrade)
        //     {
        //         User.SetUnitToNextLevel(card.unitBase.config);
        //         User.Balance -= upgradeCost;
        //         
        //         card.Refresh(card.unitBase, CardClickedInLobby, false);
        //     }
        // }
    }
}