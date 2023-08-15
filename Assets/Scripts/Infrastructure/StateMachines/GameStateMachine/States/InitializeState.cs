using Infrastructure.Logic;
using Zenject;

namespace Infrastructure.StateMachines.GameStateMachine.States
{
    public class InitializeState : IGameState
    {
        private IGameStateMachine _stateMachine;
        private LevelBuilder _levelBuilder;

        public InitializeState(IGameStateMachine stateMachine, LevelBuilder levelBuilder)
        {
            _levelBuilder = levelBuilder;
            _stateMachine = stateMachine;
        }
        
        public void Enter()
        {
            BuildLevel();
            GoToGameIdleState();
        }

        private void GoToGameIdleState() =>
            _stateMachine.EnterState<GameIdleStateState>();

        private void BuildLevel() =>
            _levelBuilder.BuildLevel();

        public void Exit()
        {
        }

        public class Factory : PlaceholderFactory<IGameStateMachine, InitializeState>
        {
        }
    }
}