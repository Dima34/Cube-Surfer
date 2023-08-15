using UnityEngine;
using Zenject;

namespace Infrastructure.Logic.Player
{
    public class SpawnTextOnPickup : MonoBehaviour
    {
        [SerializeField] private Transform _spawnFromTransform;
        [SerializeField] private CubeHolder _cubeHolder;
        private LevelBuilder _levelBuilder;

        [Inject]
        public void Construct(LevelBuilder levelBuilder) =>
            _levelBuilder = levelBuilder;

        private void Start() =>
            _cubeHolder.OnCubeAdded += SpawnText;

        private void OnDestroy() =>
            _cubeHolder.OnCubeAdded -= SpawnText;

        private void SpawnText() =>
            _levelBuilder.SpawnPickupText(_spawnFromTransform);
    }
}