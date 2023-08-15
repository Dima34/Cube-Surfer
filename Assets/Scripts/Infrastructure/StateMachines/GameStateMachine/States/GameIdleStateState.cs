using Infrastructure.Factories.UI;
using Infrastructure.Logic.UI;
using UnityEngine;
using Zenject;

namespace Infrastructure.StateMachines.GameStateMachine.States
{
    public class GameIdleStateState : IGameState
    {
        private IUIFactory _uiFactory;
        private IGameStateMachine _gameStateMachine;
        private WaitForStartHUD _waitingForStartHUD;

        public GameIdleStateState(IUIFactory uiFactory, IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
            _uiFactory = uiFactory;
        }

        public void Enter()
        {
            _waitingForStartHUD = _uiFactory.SpawnStartWaitingHUD();
            _waitingForStartHUD.OnHold += EnterGameLoop;
        }

        private void EnterGameLoop()
        {
            _waitingForStartHUD.OnHold -= EnterGameLoop;
            _gameStateMachine.EnterState<GameLoopState>();
        }

        public void Exit() =>
            _uiFactory.RemoveStartWaitingHUD();

        public class Factory : PlaceholderFactory<IGameStateMachine, GameIdleStateState>
        {
        }
    }
}