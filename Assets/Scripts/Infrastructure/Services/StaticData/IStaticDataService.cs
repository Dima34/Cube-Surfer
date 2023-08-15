using StaticData;

namespace Infrastructure.Services.StaticData
{
    public interface IStaticDataService
    {
        LevelStaticData GetCurrentLevelData();
        PlayerStaticData GetPlayerData();
        WallsProviderStaticData GetWallsProviderStaticData();
    }
}