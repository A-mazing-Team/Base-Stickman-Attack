using _Scripts.Managers;
using UnityEngine;

namespace _Scripts.Save
{
    public static class User
    {
        private const string LevelDataName = "Level";
        private const string UnitLevelDataName = "Level";

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

        public static int GetUnitLevel(UnitBase unitBase)
        {
            return PlayerPrefs.GetInt(UnitLevelDataName + unitBase.config.name, 0);
        }

        public static void SetUnitLevel(UnitBase unitBase, int level)
        {
            PlayerPrefs.SetInt(UnitLevelDataName + unitBase.config.name, level);
        }
        
    }
}