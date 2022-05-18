using _Scripts.Battle;
using UnityEngine;
using Zenject;

namespace _Scripts.Installer
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField]
        private BattleManager _battleManager;
        
        public override void InstallBindings()
        {
            Container.Bind<BattleManager>().To<BattleManager>().FromInstance(_battleManager).AsSingle();
        }
    }
}