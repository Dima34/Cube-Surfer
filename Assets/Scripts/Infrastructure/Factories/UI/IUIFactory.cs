using Infrastructure.Logic.UI;

namespace Infrastructure.Factories.UI
{
    public interface IUIFactory
    {
        WaitForStartHUD SpawnStartWaitingHUD();
        void RemoveStartWaitingHUD();
        EndgameHUD SpawnEndgameHUD();
        void RemoveEndgameHUD();
    }
}