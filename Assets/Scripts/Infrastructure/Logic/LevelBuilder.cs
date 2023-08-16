using Cinemachine;
using Infrastructure.Factories;
using Infrastructure.Logic.Player;
using Infrastructure.Services.StaticData;
using StaticData;
using UnityEngine;
using Zenject;

namespace Infrastructure.Logic
{
    public class LevelBuilder
    {
        private IGameFactory _gameFactory;
        private PlayerStaticData _playerStaticData;
        private LevelStaticData _levelStaticData;
        private CubeHolder _playerCubeHolder;

        [Inject]
        public void Construct(IGameFactory gameFactory, IStaticDataService staticDataService)
        {
            _playerStaticData = staticDataService.GetPlayerData();
            _levelStaticData = staticDataService.GetCurrentLevelData();
            _gameFactory = gameFactory;
        }

        public void BuildLevel()
        {
            GameObject player = CreatePlayer();
            GenerateLevel();
            CreateTrail(player);
            CreateCamera(player.gameObject);
            CreateSectionRespawner(player.gameObject);
            CreateWarpEffectDisabled(player.gameObject);
        }

        private GameObject CreatePlayer()
        {
            GameObject player = _gameFactory.SpawnPlayerAndRegisterCubeHolder();
            player.transform.position = _levelStaticData.PlayerSpawnPoint;

            PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
            playerMovement.Initialize(
                _playerStaticData.ForwardSpeed,
                _playerStaticData.SideMovementFactor,
                _levelStaticData.TrackWidth);

            return player;
        }

        private void GenerateLevel()
        {
            SpawnStartSection();

            for (int i = 0; i < _levelStaticData.MaxSections - 1; i++)
                SpawnLevelSection();
        }

        private void CreateTrail(GameObject player)
        {
            GameObject trail = _gameFactory.CreateTrail();
            trail.transform.position = player.transform.position;
        }

        private void SpawnStartSection()
        {
            LevelSection levelSection = SpawnSection(_levelStaticData.StartSectionPosition);
            levelSection.CreateGround();
        }

        private GameObject SpawnLevelSection()
        {
            Transform lastSectionTransform = GetLastActiveSectionTransform();
            Vector3 newSectionPosition = GetNextSpawnPosition(lastSectionTransform);

            LevelSection levelSection = SpawnSection(newSectionPosition);
            levelSection.CreateGround();
            levelSection.CreateCollectableCubes();
            levelSection.CreateWall();

            return levelSection.gameObject;
        }

        private Transform GetLastActiveSectionTransform() =>
            _gameFactory.ActiveLevelSections[_gameFactory.ActiveLevelSections.Count - 1].transform;

        private Vector3 GetNextSpawnPosition(Transform lastSectionTransform) =>
            lastSectionTransform.position + lastSectionTransform.forward * _levelStaticData.TrackLength;

        private LevelSection SpawnSection(Vector3 at)
        {
            GameObject levelSectionObject = _gameFactory.SpawnSectionAndAddToActiveList();
            levelSectionObject.transform.position = at;
            return levelSectionObject.GetComponentInChildren<LevelSection>();
        }

        private void CreateCamera(GameObject player)
        {
            CinemachineVirtualCamera camera = _gameFactory.SpawnCameraAndBindCameraChaker();
            camera.Follow = player.transform;
        }

        private void CreateSectionRespawner(GameObject player)
        {
            GameObject sectionRespawner = _gameFactory.SpawnSectionRespawner();
            SetupObjectFollower(player, sectionRespawner, _levelStaticData.SectionRespawnerOffset);
        }

        private void CreateWarpEffectDisabled(GameObject player)
        {
            GameObject warpEffect = _gameFactory.SpawnWarpEffectDisabled();
            SetupObjectFollower(player, warpEffect, _levelStaticData.WarpEffectOffset);
        }

        private void SetupObjectFollower(GameObject objectToFollow, GameObject objectToSetup, Vector3 offset)
        {
            ObjectFollower objectFollower = objectToSetup.GetComponent<ObjectFollower>();
            objectFollower.Initialize(objectToFollow.transform, offset);
        }

        public void RemoveSection(GameObject gameObject)
        {
            for (var i = 0; i < _gameFactory.ActiveLevelSections.Count; i++)
            {
                GameObject levelSection = _gameFactory.ActiveLevelSections[i];

                if (levelSection == gameObject) 
                    _gameFactory.DestroySection(levelSection);
            }
        }

        public void SpawnLevelSectionIfNotEnoughAnim()
        {
            if (_gameFactory.ActiveLevelSections.Count < _levelStaticData.MaxSections)
            {
                GameObject section = SpawnLevelSection();
                PlaySectionAnim(section);
            }
        }

        private void PlaySectionAnim(GameObject section) =>
            section.GetComponentInChildren<Animation>().enabled = true;

        public void SpawnPickupText(Transform parentTransform)
        {
            CubePickupText pickupText = _gameFactory.SpawnCubePickupText();
            
            pickupText.transform.SetParent(parentTransform);
            pickupText.transform.position = parentTransform.position;
        }

        public void CleanupLevel()
        {
            DestroyPlayer();
            DestroyAllPickupText();
            DestroyLevel();
            DestroyTrail();
            DestroyCamera();
            DestroySectionRespawner();
            DestroyWarpEffect();
        }

        private void DestroyWarpEffect() =>
            _gameFactory.DestroyWarpEffect();

        private void DestroyPlayer() =>
            _gameFactory.DestroyPlayerAndUnbindCubeHolder();

        private void DestroyAllPickupText() =>
            _gameFactory.DestroyAllCubePickupText();

        private void DestroyLevel() =>
            _gameFactory.DestroyAllASections();

        private void DestroyTrail() =>
            _gameFactory.DestroyTrail();

        private void DestroyCamera() =>
            _gameFactory.DestroyCameraAndUnbindCameraShaker();

        private void DestroySectionRespawner() =>
            _gameFactory.DestroySectionRespawner();

        public void DestroySpawnText(CubePickupText text) =>
            _gameFactory.DestroyCubePickupText(text);
    }
}