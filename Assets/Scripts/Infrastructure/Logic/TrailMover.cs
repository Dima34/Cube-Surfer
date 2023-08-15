using Infrastructure.Factories;
using Infrastructure.Services.StaticData;
using StaticData;
using UnityEngine;
using Zenject;

namespace Infrastructure.Logic
{
    public class TrailMover : MonoBehaviour
    {
        private float _yPosition;
        private GameObject _player;

        private const float ABOVE_GROUND_DISTANCE = 0.1f;

        [Inject]
        public void Construct(IGameFactory gameFactory, StaticDataService staticDataService)
        {
            _player = gameFactory.Player;
            CalculateYPosition(staticDataService);
        }

        private void CalculateYPosition(StaticDataService staticDataService)
        {
            LevelStaticData currentLevelData = staticDataService.GetCurrentLevelData();
            _yPosition = currentLevelData.StartSectionPosition.y + (currentLevelData.TrackHeight / 2 + ABOVE_GROUND_DISTANCE);
        }

        private void FixedUpdate() =>
            UpdatePostion();

        private void UpdatePostion() =>
            transform.position = new Vector3(_player.transform.position.x, _yPosition, _player.transform.position.z);
    }
}