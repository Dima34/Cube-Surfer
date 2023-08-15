using Infrastructure.Factories;
using Infrastructure.Logic.Player;
using Unity.VisualScripting;
using Zenject;

namespace Infrastructure.StateMachines.GameStateMachine.States
{
    public class GameLoopState : IGameState
    {
        private IGameFactory _gameFactory;
        private PlayerMovement _playerMovement;
        private IGameStateMachine _gameStateMachine;
        private CubeHolder _cubeHolder;
        private ColliderOnWallTouchEndGame _stickmanOnTouchCollider;

        public GameLoopState(IGameFactory gameFactory, IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
            _gameFactory = gameFactory;
        }

        public void Enter() =>
            RunPlayer();

        private void RunPlayer()
        {
            _playerMovement = _gameFactory.Player.GetComponent<PlayerMovement>();
            _cubeHolder = _gameFactory.Player.GetComponentInChildren<CubeHolder>();
            _stickmanOnTouchCollider = _gameFactory.Player.GetComponentInChildren<ColliderOnWallTouchEndGame>();

            StartPlayerMoving();
            _cubeHolder.OnGameEnd += GoToEndgameState;
            _stickmanOnTouchCollider.OnGameEnd += GoToEndgameState;
        }

        private void GoToEndgameState() =>
            _gameStateMachine.EnterState<EndGameState>();

        public void Exit()
        {
            _cubeHolder.OnGameEnd -= GoToEndgameState;
            _stickmanOnTouchCollider.OnGameEnd -= GoToEndgameState;

            StopPlayerMoving();
        }

        private void StartPlayerMoving() =>
            _playerMovement.StartMoving();

        private void StopPlayerMoving() =>
            _playerMovement.StopMoving();

        public class Factory : PlaceholderFactory<IGameStateMachine, GameLoopState>
        {
        }
    }
}