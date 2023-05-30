using DefaultNamespace.Infrastructure;
using DefaultNamespace.Infrastructure.Services;
using Infrastructure.AssetManagment;
using Infrastructure.Factory;
using Infrastructure.Services;
using Infrastructure.Services.Input;
using Infrastructure.Services.PersistentProgress;
using Infrastructure.Services.Randomizer;
using Infrastructure.Services.SaveLoad;
using Logic.StaticData;
using UnityEngine;

namespace Infrastructure.States
{
    public class BootstrapState : IState
    {
        private const string Init = "Init";
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _services = services;

            RegisterServices();
        }

        public void Enter()
        {
            _sceneLoader.Load(Init, onLoaded: EnterLoadLevel);
        }

        public void Exit()
        {
        }

        private void EnterLoadLevel() =>
            _stateMachine.Enter<LoadProgressState>();

        private void RegisterServices()
        {
            RegisterStaticData();

            _services.RegisterSingle<IInputService>(InputService());
            _services.RegisterSingle<IRandomService>(new RandomService());
            _services.RegisterSingle<IAssets>(new AssetsProvider());
            
            var persistentProgressService = new PersistentProgressService();
            _services.RegisterSingle<IPersistentProgressService>(persistentProgressService);
            
            _services.RegisterSingle<IGameFactory>(new GameFactory(_services.Single<IAssets>(),
                _services.Single<IStaticDataService>(), _services.Single<IRandomService>(),
                persistentProgressService));
            _services.RegisterSingle<ISaveLoadService>(
                new SaveLoadService(_services.Single<IPersistentProgressService>(),
                    _services.Single<IGameFactory>()));
        }

        private void RegisterStaticData()
        {
            IStaticDataService staticData = new StaticDataService();
            staticData.LoadMonsters();
            _services.RegisterSingle(staticData);
        }

        private static IInputService InputService() =>
            Application.isEditor ? new StandaloneInputService() : new MobileInputService();
    }
}