using UnityEngine;

namespace Infrastructure.Services.WallsProvider
{
    public interface IWallsProviderService
    {
        GameObject GetRandomWallVariant();
    }
}