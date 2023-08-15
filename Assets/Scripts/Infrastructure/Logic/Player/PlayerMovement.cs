using Infrastructure.Installer;
using Infrastructure.Services.Input;
using UnityEngine;
using Zenject;

namespace Infrastructure.Logic.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : MonoBehaviour
    {
        private float _forwardSpeed;
        private float _sideMovementFactor;
        private float _trackHalfWidth;
        private float _currentPercent = 0;
        private float _previousDistance = 0;
        private IInputService _inputService;
        private float _playerPercentOnTrack;
        private bool _canMoving;

        private void FixedUpdate()
        {
            if (_canMoving)
            {
                AddForwardVelocity();
                AddSideForce();
            }
        }

        [Inject]
        public void Construct(IInputService inputService) =>
            _inputService = inputService;

        public void Initialize(float forwardSpeed, float sideMovementFactor, float trackWidth)
        {
            _forwardSpeed = forwardSpeed;
            _sideMovementFactor = sideMovementFactor;
            _trackHalfWidth = trackWidth / 2;

            CalculatePlayerPercentOnTrack(trackWidth);
        }

        private void CalculatePlayerPercentOnTrack(float trackWidth)
        {
            float playerWidth = transform.localScale.x;
            _playerPercentOnTrack = playerWidth / trackWidth * 100;
        }

        private void AddForwardVelocity() =>
            transform.position += transform.forward * _forwardSpeed * Time.deltaTime;

        private void AddSideForce()
        {
            TryAddPercent(_inputService.XAxis);
            float onTrackDistance = GetDistanceOnTrack(_currentPercent);
            transform.position = new Vector3(onTrackDistance, transform.position.y, transform.position.z);
        }

        private void TryAddPercent(float onScreenMovePercent)
        {
            float newPercent = _currentPercent + onScreenMovePercent * _sideMovementFactor;

            float newPercentWithPlayerSize = Mathf.Abs(newPercent) + _playerPercentOnTrack;
            if (newPercentWithPlayerSize <= 100)
                _currentPercent = newPercent;
        }

        private float GetDistanceOnTrack(float percent)
        {
            if (percent == 0f)
                return _previousDistance;

            float distanceOnTrack = _trackHalfWidth * (percent / 100);
            _previousDistance = distanceOnTrack;
            return distanceOnTrack;
        }
        
        public void StartMoving() =>
            _canMoving = true;

        public void StopMoving() =>
            _canMoving = false;
    }
}