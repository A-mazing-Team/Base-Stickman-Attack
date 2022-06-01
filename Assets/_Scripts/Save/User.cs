using System;
using _Scripts.Managers;
using UnityEngine;

namespace _Scripts.Save
{
    public static class User
    {
        public static event Action BalanceChanged = null;
        
        private const int MaxBattleDeckCount = 4;
        private const string LevelDataName = "Level";
        private const string UnitLevelDataName = "UnitLevel";
        private const string BattleDeckData = "Deck";
        private const string BalanceDataName = "Balance";
        private const string DecklenghtData = "DeckLenght";


        public static int Level
        {
            get => PlayerPrefs.GetInt(LevelDataName, 0);
            set
            {
                //TODO:DELETE
                if (value == 5)
                {
                    value = 4;
                }
                PlayerPrefs.SetInt(LevelDataName, value);
            } 
        }

        public static int Balance
        {
            get => PlayerPrefs.GetInt(BalanceDataName, 0);
            
            set
            {
                PlayerPrefs.SetInt(BalanceDataName, value);
                BalanceChanged?.Invoke();
            } 
        }

        public static int GetUnitLevel(UnitConfig config)
        {
            return PlayerPrefs.GetInt(UnitLevelDataName + config.name, 0);
        }

        public static void SetUnitToNextLevel(UnitConfig config)
        {
            int level = GetUnitLevel(config);
            PlayerPrefs.SetInt(UnitLevelDataName + config.name, level + 1);
        }
        
        

        public static string[] GetBattleDeckUnitNames()
        {
            int lenght = PlayerPrefs.GetInt(DecklenghtData);
            
            string[] names = new string[lenght];
            
            for (int i = 0; i < lenght; i++)
            {
                names[i] = PlayerPrefs.GetString(BattleDeckData + i.ToString());
            }

            return names;
        }

        public static void UpdateBattleDeck(UnitConfig[] updatedUnits)
        {
            for (int i = 0; i < updatedUnits.Length; i++)
            {
                PlayerPrefs.SetString(BattleDeckData + i.ToString(), updatedUnits[i].name);
            }
            
            PlayerPrefs.SetInt(DecklenghtData, updatedUnits.Length);
        }
        
    }
}