using System.Collections.Generic;
using Cinemachine;
using Infrastructure.Constants;
using Infrastructure.Logic;
using Infrastructure.Logic.Player;
using Infrastructure.Services.WallsProvider;
using UnityEngine;
using Zenject;

namespace Infrastructure.Factories
{
    public class GameFactory : IGameFactory
    {
        private List<GameObject> _activeLevelSections = new List<GameObject>();
        private DiContainer _container;
        private IWallsProviderService _wallsProviderService;
        private GameObject _player;
        private GameObject _camera;
        private GameObject _sectionRespawner;
        private GameObject _trail;
        private List<CubePickupText> _cubePickupTexts = new List<CubePickupText>();

        public List<GameObject> ActiveLevelSections => _activeLevelSections;
        public GameObject Player => _player;

        public GameFactory(DiContainer container, IWallsProviderService wallsProviderService)
        {
            _wallsProviderService = wallsProviderService;
            _container = container;
        }

        public GameObject SpawnPlayerAndRegisterCubeHolder()
        {
            _player = _container.InstantiatePrefabResource(ResourcePaths.PLAYER);
            BindCubeHolder(_player);

            return _player;
        }

        public void DestroyPlayer()
        {
            _container.Unbind<CubeHolder>();
            Object.Destroy(_player);
        }

        private void BindCubeHolder(GameObject player)
        {
            CubeHolder cubeHolder = player.GetComponentInChildren<CubeHolder>();
            _container
                .Bind<CubeHolder>()
                .FromInstance(cubeHolder)
                .AsCached();
        }

        public GameObject SpawnSectionRespawner()
        {
            _sectionRespawner = _container.InstantiatePrefabResource(ResourcePaths.SECTION_RESPAWNER);
            return _sectionRespawner;
        }

        public void DestroySectionRespawner() =>
            Object.Destroy(_sectionRespawner);

        public GameObject CreateTrail()
        {
            _trail = _container.InstantiatePrefabResource(ResourcePaths.TRAIL);
            return _trail;
        }

        public void DestroyTrail() =>
            Object.Destroy(_trail);

        public CinemachineVirtualCamera SpawnCamera()
        {
            _camera = _container
                .InstantiatePrefabResource(ResourcePaths.CAMERA);
            
            CinemachineVirtualCamera virtualCamera = _camera.GetComponentInChildren<CinemachineVirtualCamera>();
            return virtualCamera;
        }

        public void DestroyCamera() =>
            Object.Destroy(_camera.gameObject);

        public GameObject SpawnSectionAndAddToActiveList()
        {
            GameObject levelSection = _container
                .InstantiatePrefabResource(ResourcePaths.SECTION);

            _activeLevelSections.Add(levelSection);

            return levelSection;
        }

        public void DestroySection(GameObject levelSection)
        {
            _activeLevelSections.Remove(levelSection);
            Object.Destroy(levelSection);
        }

        public void DestroyAllASections()
        {
            for (var i = _activeLevelSections.Count - 1; i >= 0; i--)
                Object.Destroy(_activeLevelSections[i].gameObject);

            _activeLevelSections = new List<GameObject>();
        }

        public GameObject SpawnGround() =>
            _container.InstantiatePrefabResource(ResourcePaths.GROUND);

        public GameObject CreateCollectableCube() =>
            _container.InstantiatePrefabResource(ResourcePaths.CUBE_COLLECTABLE);

        public GameObject SpawnCollectedCube() =>
            _container.InstantiatePrefabResource(ResourcePaths.CUBE_COLLECTED);

        public GameObject SpawnRandomWall()
        {
            GameObject randomWall = _wallsProviderService.GetRandomWallVariant();
            return _container.InstantiatePrefab(randomWall);
        }

        public CubePickupText SpawnCubePickupText()
        {
            CubePickupText text = _container.InstantiatePrefabResource(ResourcePaths.CUBE_COLLECTED_TEXT).GetComponent<CubePickupText>();
            _cubePickupTexts.Add(text);
            
            return text;
        }

        public void DestroyAllCubePickupText()
        {
            for (var i = _cubePickupTexts.Count - 1; i >= 0; i--)
                DestroyCubePickupText(_cubePickupTexts[i]);
        }
        
        public void DestroyCubePickupText(CubePickupText text)
        {
            _cubePickupTexts.Remove(text);
            Object.Destroy(text.gameObject);
        }
    }
}