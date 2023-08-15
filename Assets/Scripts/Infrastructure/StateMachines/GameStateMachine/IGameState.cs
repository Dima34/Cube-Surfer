namespace Infrastructure.StateMachines.GameStateMachine.States
{
    public interface IGameState
    {
        void Enter();
        void Exit();
    }
}