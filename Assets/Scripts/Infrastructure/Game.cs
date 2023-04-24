using CodeBase.Logic;
using DefaultNamespace.Infrastructure;
using DefaultNamespace.Infrastructure.Services;
using Services.Input;

public class Game
{
    public GameStateMachine StateMachine;

    public Game(ICoroutineRunner coroutineRunner, LoadingCurtain loadingCurtain)
    {
        StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), loadingCurtain, AllServices.Container);
    }

   
}