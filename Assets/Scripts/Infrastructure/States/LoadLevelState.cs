using DefaultNamespace.Infrastructure;
using DefaultNamespace.UI;
using Hero;
using Infrastructure.Factory;
using Infrastructure.Services.PersistentProgress;
using Logic;
using Logic.CameraLogic;
using UnityEngine;

namespace Infrastructure.States
{
    public class LoadLevelState : IPayLoadedState<string>
    {
        private const string _initPointTag = "InitPoint";
        private const string EnemySpawnerTag = "EnemySpawner";

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _curtain;
        private IGameFactory _gameFactory;
        private readonly IPersistentProgressService _progressService;

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain curtain,
            IGameFactory gameFactory, IPersistentProgressService progressService)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _gameFactory = gameFactory;
            _progressService = progressService;
        }

        public void Enter(string sceneName)
        {
            _curtain.Show();
            _gameFactory.Cleanup();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit() =>
            _curtain.Hide();

        private void OnLoaded()
        {
            InitGameWorld();
            InformProgressReaders();
            _stateMachine.Enter<GameLoopState>();
        }

        private void InformProgressReaders()
        {
            foreach (var progressReader in _gameFactory.ProgressReaders)
            {
                progressReader.LoadProgress(_progressService.Progress);
            }
        }

        private void InitGameWorld()
        {
            InitSpawners();
            
            var hero = InitHero();
         
            InitHud(hero);

            CameraFollow(hero);
        }

        private void InitSpawners()
        {
            foreach (var spawnerObject in GameObject.FindGameObjectsWithTag(EnemySpawnerTag))
            {
              var spawner = spawnerObject.GetComponent<EnemySpawner>();
              _gameFactory.Register(spawner);
            }
        }

        private GameObject InitHero() => 
            _gameFactory.CreateHero(GameObject.FindWithTag(_initPointTag));

        private void InitHud(GameObject hero)
        {
           GameObject hud = _gameFactory.CreateHUD();
           
           hud.GetComponentInChildren<ActorUI>().Construct(hero.GetComponent<HeroHealth>());
        }

        private static void CameraFollow(GameObject hero) =>
            Camera.main.GetComponent<CameraFollow>().Follow(hero);
    }
}