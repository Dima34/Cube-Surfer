using Infrastructure.Factories;
using Infrastructure.Logic.Player;
using Infrastructure.Services.Death;
using Unity.VisualScripting;
using Zenject;

namespace Infrastructure.StateMachines.GameStateMachine.States
{
    public class GameLoopState : IGameState
    {
        private IGameFactory _gameFactory;
        private PlayerMovement _playerMovement;
        private IGameStateMachine _gameStateMachine;
        private IDeathService _deathService;

        public GameLoopState(IGameFactory gameFactory, IGameStateMachine gameStateMachine, IDeathService deathService)
        {
            _gameStateMachine = gameStateMachine;
            _deathService = deathService;
            _gameFactory = gameFactory;
        }

        public void Enter() =>
            RunGame();

        private void RunGame()
        {
            _playerMovement = _gameFactory.Player.GetComponent<PlayerMovement>();

            EnableWarpEffect();
            StartPlayerMoving();

            _deathService.Happend += GoToEndgameState;
        }

        public void Exit() =>
            StopGame();

        private void StopGame()
        {
            _deathService.Happend -= GoToEndgameState;

            StopPlayerMoving();
        }

        private void StartPlayerMoving() =>
            _playerMovement.StartMoving();

        private void EnableWarpEffect() =>
            _gameFactory.WarpEffect.SetActive(true);

        private void StopPlayerMoving() =>
            _playerMovement.StopMoving();

        private void GoToEndgameState() =>
            _gameStateMachine.EnterState<EndGameState>();

        public class Factory : PlaceholderFactory<IGameStateMachine, GameLoopState>
        {
        }
    }
}