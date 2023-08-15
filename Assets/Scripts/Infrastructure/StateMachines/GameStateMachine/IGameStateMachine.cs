using Infrastructure.StateMachines.GameStateMachine.States;
using Zenject;

namespace Infrastructure.StateMachines.GameStateMachine
{
    public interface IGameStateMachine
    {
        void EnterState<TState>() where TState : IGameState;
    }
}