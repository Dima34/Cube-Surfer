using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.Factories;
using Infrastructure.Services.CoroutineRunner;
using Infrastructure.Services.Death;
using Infrastructure.Services.StaticData;
using Infrastructure.StateMachines.GameStateMachine;
using Infrastructure.StateMachines.GameStateMachine.States;
using StaticData;
using UnityEngine;
using Zenject;

namespace Infrastructure.Logic.Player
{
    public class CubeHolder : MonoBehaviour
    {
        [SerializeField] private Transform _stickman;

        private ICoroutineRunnerService _coroutineRunnerService;
        private List<GameObject> _activeCubes = new List<GameObject>();
        private IGameFactory _gameFactory;
        private bool _isAddingProcess;
        private IGameStateMachine _gameStateMachine;
        private EndGameState _endGameState;
        private float _floorYCoordinate;
        private IDeathService _deathService;
        public event Action OnCubeAdded;
        
        private const float SECURITY_Y_SPAWN_OFFSET = 0.05f;

        [Inject]
        public void Construct(ICoroutineRunnerService coroutineRunnerService, IGameFactory gameFactory, IStaticDataService staticDataService, IDeathService deathService)
        {
            _deathService = deathService;
            _gameFactory = gameFactory;
            _coroutineRunnerService = coroutineRunnerService;

            LevelStaticData currentLevelData = staticDataService.GetCurrentLevelData();
            CalculateFloorYCoordinate(currentLevelData);
        }

        private void CalculateFloorYCoordinate(LevelStaticData currentLevelData) =>
            _floorYCoordinate = currentLevelData.StartSectionPosition.y + currentLevelData.TrackHeight / 2;

        private void Start() =>
            AddCubeSilent();

        public void AddCube()
        {
            if (!_isAddingProcess)
                _coroutineRunnerService.Run(AddCubeProcess());
        }

        public void AddCubeSilent()
        {
            if (!_isAddingProcess)
                _coroutineRunnerService.Run(AddCubeSilentProcess());
        }

        private IEnumerator AddCubeProcess()
        {
            _isAddingProcess = true;
            yield return null;

            SpawnCube();
            InvokeCubeAddedEvent();

            _isAddingProcess = false;
        }

        private IEnumerator AddCubeSilentProcess()
        {
            _isAddingProcess = true;
            yield return null;

            SpawnCube();

            _isAddingProcess = false;
        }

        private void SpawnCube()
        {
            MoveStickmanOnTop();
            
            GameObject spawnedCube = SpawnCollectedCube();
            _activeCubes.Add(spawnedCube);
        }
        private void MoveStickmanOnTop()
        {
            Vector3 oldPos = _stickman.transform.position;
            _stickman.transform.position = new Vector3(oldPos.x, _floorYCoordinate +_activeCubes.Count + 1 + SECURITY_Y_SPAWN_OFFSET, oldPos.z);
        }

        private GameObject SpawnCollectedCube()
        {
            GameObject cube = _gameFactory.SpawnCollectedCube();

            float cubeHeight = cube.transform.localScale.y;
            Vector3 spawnPosition = GetCubeSpawnPoint(cubeHeight);

            cube.transform.SetParent(transform);
            cube.transform.position = spawnPosition;

            return cube;
        }

        private Vector3 GetCubeSpawnPoint(float cubeHeight)
        {
            Vector3 prevObjPosition = transform.position;

            if (_activeCubes.Count > 0)
                prevObjPosition = _activeCubes.Last().transform.position;

            return GetYOffsetPosBasedOnPrevObject(cubeHeight, prevObjPosition);
        }

        private static Vector3 GetYOffsetPosBasedOnPrevObject(float cubeHeight, Vector3 previousObjPos) =>
            new(previousObjPos.x, previousObjPos.y + cubeHeight / 2 + SECURITY_Y_SPAWN_OFFSET, previousObjPos.z);

        public void RemoveCube(GameObject cubeToRemove)
        {
            int cubeToRemoveIndex = _activeCubes.IndexOf(cubeToRemove);
            int lastCubeIndex = _activeCubes.Count - 1;
            
            if (cubeToRemoveIndex == lastCubeIndex)
                EndGame();

            _activeCubes.Remove(cubeToRemove);
        }

        private void EndGame() =>
            _deathService.Die();
        
        private void InvokeCubeAddedEvent() =>
            OnCubeAdded?.Invoke();
    }
}