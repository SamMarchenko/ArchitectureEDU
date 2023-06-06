using Infrastructure.Services;
using Infrastructure.States;

namespace Infrastructure
{
    public interface IGameStateMachine : IService
    {
        void Enter<TState>() where TState : class, IState;
        void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayLoadedState<TPayload>;
    }
}