using Infrastructure.Factories.UI;
using Infrastructure.Logic.UI;
using Zenject;

namespace Infrastructure.StateMachines.GameStateMachine.States
{
    public class EndGameState : IGameState
    {
        private IUIFactory _uiFactory;
        private IGameStateMachine _gameStateMachine;
        private EndgameHUD _endgameHUD;

        public EndGameState(IUIFactory uiFactory, IGameStateMachine gameStateMachine)
        {
            _uiFactory = uiFactory;
            _gameStateMachine = gameStateMachine;
        }

        public void Enter()
        {
            _endgameHUD = SpawnEndgameHUD();
            _endgameHUD.OnTryAgainClick += GoToCleanupState;
        }

        private void GoToCleanupState()
        {
            _endgameHUD.OnTryAgainClick -= GoToCleanupState;
            _gameStateMachine.EnterState<CleanUpLevelState>();
        }

        private EndgameHUD SpawnEndgameHUD() =>
            _uiFactory.SpawnEndgameHUD();

        public void Exit() =>
            _uiFactory.RemoveEndgameHUD();

        public class Factory : PlaceholderFactory<IGameStateMachine, EndGameState>
        {
        }
    }
}