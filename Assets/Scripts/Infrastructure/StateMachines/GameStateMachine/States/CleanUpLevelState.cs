using Infrastructure.Logic;
using Zenject;

namespace Infrastructure.StateMachines.GameStateMachine.States
{
    public class CleanUpLevelState : IGameState
    {
        private readonly LevelBuilder _levelBuilder;
        private readonly IGameStateMachine _gameStateMachine;

        public CleanUpLevelState(IGameStateMachine gameStateMachine, LevelBuilder levelBuilder)
        {
            _levelBuilder = levelBuilder;
            _gameStateMachine = gameStateMachine;
        }
        
        public void Enter()
        {
            CleanupLevel();
            GoToInitializeState();
        }

        private void CleanupLevel()
        {
            _levelBuilder.CleanupLevel();
        }

        private void GoToInitializeState() =>
            _gameStateMachine.EnterState<InitializeState>();

        public void Exit()
        {
        }

        public class Factory : PlaceholderFactory<IGameStateMachine, CleanUpLevelState>
        {
        }
    }
} 