using System.Collections.Generic;
using System.Linq;
using Infrastructure.Constants;
using Infrastructure.Services.SceneLoad;
using StaticData;
using UnityEngine;
using Zenject;

namespace Infrastructure.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private Dictionary<string, LevelStaticData> _levelsData;
        private ISceneLoadService _sceneLoadService;
        private PlayerStaticData _playerData;
        private WallsProviderStaticData _wallsProviderData;

        [Inject]
        public void Construct(ISceneLoadService sceneLoadService)
        {
            _sceneLoadService = sceneLoadService;
            
            LoadData();
        }

        private void LoadData()
        {
            LoadLevelData();
            LoadPlayerData();
            LoadWallsProviderData();
        }

        private void LoadWallsProviderData() =>
            _wallsProviderData = Resources.Load<WallsProviderStaticData>(ResourcePaths.WALLS_PROVIDER);

        private void LoadLevelData() =>
            _levelsData = Resources.LoadAll<LevelStaticData>(ResourcePaths.LEVELS_STATICDATA)
                .ToDictionary(x => x.LevelName, x => x);

        private void LoadPlayerData() =>
            _playerData = Resources.Load<PlayerStaticData>(ResourcePaths.PLAYER_STATICDATA);

        public LevelStaticData GetCurrentLevelData()
        {
            string currentLevel = _sceneLoadService.GetCurrentLevel();
            return _levelsData[currentLevel];
        }

        public PlayerStaticData GetPlayerData() =>
            _playerData;

        public WallsProviderStaticData GetWallsProviderStaticData() =>
            _wallsProviderData;
    }
}