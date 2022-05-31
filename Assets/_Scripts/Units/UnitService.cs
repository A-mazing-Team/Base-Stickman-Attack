using System.Linq;
using UnityEngine;

namespace _Scripts.Managers
{
    public class UnitService : MonoBehaviour
    {
        [SerializeField]
        private UnitConfig[] allUnitsData;

        public UnitConfig GetUnitByName(string unitName)
        {
            return allUnitsData.FirstOrDefault(i => i.name == unitName);
        }
    }
}