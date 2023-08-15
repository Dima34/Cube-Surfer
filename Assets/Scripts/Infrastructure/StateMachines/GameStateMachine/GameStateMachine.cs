using System.Collections.Generic;
using Infrastructure.StateMachines.GameStateMachine.States;

namespace Infrastructure.StateMachines.GameStateMachine
{
    public class GameStateMachine : IGameStateMachine
    {
        private IGameState _currentState;
        private Dictionary<System.Type, IGameState> _registeredStates;

        public GameStateMachine(
            InitializeState.Factory initializeStateFactory,
            GameIdleStateState.Factory gameIdleStateStateFactory,
            GameLoopState.Factory gameLoopStateFactory,
            EndGameState.Factory endGameStateFactory,
            CleanUpLevelState.Factory cleanUpLevelStatefFactory)
        {
            _registeredStates = new Dictionary<System.Type, IGameState>();

            RegisterState(initializeStateFactory.Create(this));
            RegisterState(gameIdleStateStateFactory.Create(this));
            RegisterState(gameLoopStateFactory.Create(this));
            RegisterState(endGameStateFactory.Create(this));
            RegisterState(cleanUpLevelStatefFactory.Create(this));
        }

        private void RegisterState<TState>(TState state) where TState : IGameState =>
            _registeredStates.Add(typeof(TState), state);

        public void EnterState<TState>() where TState : IGameState
        {
            if (_currentState != null)
                _currentState.Exit();

            IGameState stateToEnter = GetState<TState>();
            _currentState = stateToEnter;
            _currentState.Enter();
        }

        private IGameState GetState<TState>() where TState : IGameState =>
            _registeredStates[typeof(TState)];
    }
}