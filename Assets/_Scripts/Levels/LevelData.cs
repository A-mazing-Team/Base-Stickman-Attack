using System.Collections.Generic;
using _Scripts.Managers;
using UnityEngine;

namespace _Scripts.Levels
{
    [CreateAssetMenu]
    public class LevelData : ScriptableObject
    {
        public int allyUnitsCount;
        public GameObject enemiesPrefab;
        public UnitConfig unlockUnit;
        public int goldReward;
        public UnitConfig[] deckUnitNames;
    }
}