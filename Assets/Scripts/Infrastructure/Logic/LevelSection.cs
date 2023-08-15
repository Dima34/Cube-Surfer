using Infrastructure.Factories;
using Infrastructure.Services.Random;
using Infrastructure.Services.StaticData;
using StaticData;
using UnityEngine;
using Zenject;

namespace Infrastructure.Logic
{
    public class LevelSection : MonoBehaviour
    {
        [SerializeField] private BoxCollider _boxCollider;


        private float _groundLength;
        private IGameFactory _gameFactory;
        private LevelStaticData _currentLevelData;
        private float _safezonePercent;
        private int _blockInSectionAmount;
        private int _spawnLastBlockChance;
        private IRandomService _randomService;

        [Inject]
        public void Construct(IGameFactory gameFactory, StaticDataService staticDataService,
            IRandomService randomService)
        {
            _randomService = randomService;
            _gameFactory = gameFactory;
            _currentLevelData = staticDataService.GetCurrentLevelData();
            _safezonePercent = _currentLevelData.BlockSpawnSafezonePercentZ;
            _blockInSectionAmount = _currentLevelData.BlockInSectionAmount;
            _spawnLastBlockChance = _currentLevelData.LastBlockChance;
            _groundLength = _currentLevelData.TrackLength;
        }

        public void CreateGround()
        {
            var ground = SpawnGround();
            ground.transform.position = transform.position;
            ground.transform.localScale = new Vector3(
                _currentLevelData.TrackWidth,
                _currentLevelData.TrackHeight,
                _currentLevelData.TrackLength);

            MakeColliderAsGroundSize(ground);
        }

        private GameObject SpawnGround()
        {
            GameObject ground = _gameFactory.SpawnGround();
            ground.transform.SetParent(transform);
            return ground;
        }

        private void MakeColliderAsGroundSize(GameObject ground) =>
            _boxCollider.size = ground.transform.localScale;

        public void CreateCollectableCubes()
        {
            float groundSafezoned = _groundLength - _groundLength * _safezonePercent / 100;
            Vector3 halfSafezonedVector = transform.forward * (groundSafezoned / 2);

            Vector3 spawnEndPosition = transform.position + halfSafezonedVector;
            Vector3 spawnStartPosition = transform.position - halfSafezonedVector;

            Vector3 spawnEndStartDiff = spawnEndPosition - spawnStartPosition;
            Vector3 blockSpawnPeriod = spawnEndStartDiff / _blockInSectionAmount;

            var spawnAmount = GetCubesAmount();

            for (int i = 0; i < spawnAmount; i++)
            {
                GameObject pickupBlock = _gameFactory.CreateCollectableCube();
                pickupBlock.transform.SetParent(transform);

                Vector3 xPosition = GetRandomBlockXPosition(pickupBlock);
                Vector3 yPosition = GetBlockYPosition(pickupBlock);
                Vector3 zPosition = GetBlockZPosition(i);

                Vector3 spawnPosition = xPosition + yPosition + zPosition;
                pickupBlock.transform.position = spawnPosition;
            }

            Vector3 GetRandomBlockXPosition(GameObject pickupBlock)
            {
                float trackHalfWidth = _currentLevelData.TrackWidth / 2;
                float blockHalfSize = pickupBlock.transform.localScale.x / 2;
                float availableHalfWidth = trackHalfWidth -
                                           trackHalfWidth * (_currentLevelData.BlockSpawnSafezonePercentX / 100) -
                                           blockHalfSize;

                float randomXPosition = _randomService.GetBetween(availableHalfWidth, -availableHalfWidth);
                return new Vector3(randomXPosition, 0, 0);
            }

            Vector3 GetBlockZPosition(int blockIndex) =>
                spawnEndPosition - blockSpawnPeriod * blockIndex;

            Vector3 GetBlockYPosition(GameObject pickupBlock)
            {
                float halfTrackHeight = _currentLevelData.TrackHeight / 2;
                float halfBlockHeight = pickupBlock.transform.localScale.y / 2;

                Vector3 yStartPosition = Vector3.up * (halfTrackHeight + halfBlockHeight);
                return yStartPosition;
            }
        }

        private int GetCubesAmount()
        {
            bool spawnLastBlock = _randomService.Happend(_spawnLastBlockChance);
            int spawnAmount = spawnLastBlock ? _blockInSectionAmount : _blockInSectionAmount - 1;
            return spawnAmount;
        }

        public void CreateWall()
        {
            GameObject wall = _gameFactory.SpawnRandomWall();
            wall.transform.SetParent(transform);
            Vector3 zSpawnOffset = transform.forward * (_groundLength / 2 - wall.transform.localScale.z / 2);
            Vector3 ySpawnOffset = transform.up * (_currentLevelData.TrackHeight / 2);

            wall.transform.position = transform.position +
                                      zSpawnOffset + ySpawnOffset;
        }
    }
}