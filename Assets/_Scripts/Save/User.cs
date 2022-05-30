using UnityEngine;

namespace _Scripts.Save
{
    public static class User
    {
        public static int Level
        {
            get => PlayerPrefs.GetInt("Level", 0);
            set => PlayerPrefs.SetInt("Level", value);
        }
        
        //public static 
    }
}