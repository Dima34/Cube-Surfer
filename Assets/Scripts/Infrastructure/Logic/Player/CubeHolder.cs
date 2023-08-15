using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.Factories;
using Infrastructure.Services.CoroutineRunner;
using Infrastructure.StateMachines.GameStateMachine;
using Infrastructure.StateMachines.GameStateMachine.States;
using UnityEngine;
using Zenject;

namespace Infrastructure.Logic.Player
{
    [RequireComponent(typeof(BoxCollider))]
    public class CubeHolder : MonoBehaviour
    {
        [SerializeField] private GameObject _playerObject;

        private ICoroutineRunnerService _coroutineRunnerService;
        private List<GameObject> _activeCubes = new List<GameObject>();
        private IGameFactory _gameFactory;
        private bool _isAddingProcess;
        private IGameStateMachine _gameStateMachine;
        private EndGameState _endGameState;
        public event Action OnGameEnd;
        public event Action OnCubeAdded;

        [Inject]
        public void Construct(ICoroutineRunnerService coroutineRunnerService, IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
            _coroutineRunnerService = coroutineRunnerService;
        }

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
                _coroutineRunnerService.Run(AddCubeProcess());
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
            GameObject spawnedCube = SpawnCollectedCube();
            LiftPlayerUp(spawnedCube.transform.localScale.y);

            _activeCubes.Add(spawnedCube);
        }

        private void LiftPlayerUp(float liftAmount) =>
            _playerObject.transform.position += liftAmount * transform.up;

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
            Vector3 prevObjPosition = transform.position + transform.up * (cubeHeight / 2);

            if (_activeCubes.Count > 0)
                prevObjPosition = _activeCubes.Last().transform.position;

            return GetYOffestPosBasedOnPrevObject(cubeHeight, prevObjPosition);
        }

        private static Vector3 GetYOffestPosBasedOnPrevObject(float cubeHeight, Vector3 previousObjPos) =>
            new(previousObjPos.x, previousObjPos.y - cubeHeight, previousObjPos.z);

        public void RemoveCube(GameObject cubeToRemove)
        {
            int cubeToRemoveIndex = _activeCubes.IndexOf(cubeToRemove);
            if (cubeToRemoveIndex == 0)
                EndGame();

            _activeCubes.Remove(cubeToRemove);
        }

        private void EndGame() =>
            OnGameEnd?.Invoke();
        
        private void InvokeCubeAddedEvent() =>
            OnCubeAdded?.Invoke();
    }
}