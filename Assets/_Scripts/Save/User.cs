using _Scripts.Managers;
using UnityEngine;

namespace _Scripts.Save
{
    public static class User
    {
        private const int MaxBattleDeckCount = 4;
        private const string LevelDataName = "Level";
        private const string UnitLevelDataName = "UnitLevel";
        private const string BattleDeckData = "Level";
        

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
            string[] names = new string[MaxBattleDeckCount];
            
            for (int i = 0; i < MaxBattleDeckCount; i++)
            {
                names[i] = PlayerPrefs.GetString(BattleDeckData + i.ToString());
            }

            return names;
        }

        public static void UpdateBattleDeck(UnitConfig[] updatedUnits)
        {
            for (int i = 0; i < MaxBattleDeckCount; i++)
            {
                PlayerPrefs.SetString(BattleDeckData + i.ToString(), updatedUnits[i].name);
            }
        }
        
    }
}