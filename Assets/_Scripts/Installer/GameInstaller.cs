using _Scripts.Battle;
using _Scripts.Managers;
using _Scripts.UnitSpawner;
using UnityEngine;
using Zenject;

namespace _Scripts.Installer
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField]
        private BattleManager _battleManager;
        [SerializeField]
        private UnitProvider _unitProvider;
        [SerializeField]
        private UnitService _unitService;
        
        public override void InstallBindings()
        {
            Container.Bind<BattleManager>().To<BattleManager>().FromInstance(_battleManager).AsSingle();
            Container.Bind<UnitProvider>().To<UnitProvider>().FromInstance(_unitProvider).AsSingle();
            Container.Bind<UnitService>().To<UnitService>().FromInstance(_unitService).AsSingle();
        }
    }
}