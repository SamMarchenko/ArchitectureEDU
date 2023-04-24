using CodeBase.Logic;
using DefaultNamespace.CameraLogic;
using DefaultNamespace.Infrastructure;
using Infrastructure.Factory;
using UnityEngine;

namespace Infrastructure.States
{
    public class LoadLevelState : IPayLoadedState<string>
    {
        private const string _initPointTag = "InitPoint";

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _curtain;
        private IGameFactory _gameFactory;

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain curtain, IGameFactory gameFactory)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _gameFactory = gameFactory;
        }

        public void Enter(string sceneName)
        {
            _curtain.Show();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit() =>
            _curtain.Hide();

        private void OnLoaded()
        {
            var initialPoint = GameObject.FindWithTag(_initPointTag);

            var hero = _gameFactory.CreateHero(initialPoint);
            _gameFactory.CreateHUD();

            CameraFollow(hero);

            _stateMachine.Enter<GameLoopState>();
        }

        private static void CameraFollow(GameObject hero) =>
            Camera.main.GetComponent<CameraFollow>().Follow(hero);
    }
}