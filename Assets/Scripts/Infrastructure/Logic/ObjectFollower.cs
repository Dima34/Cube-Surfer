using UnityEngine;

namespace Infrastructure.Logic
{
    public class ObjectFollower : MonoBehaviour
    {
        private Transform _objectToFollow;
        private float _startYPosition;
        
        [SerializeField] private Vector3 _followOffset;

        public void Initialize(Transform objectToFollow, Vector3 followOffset)
        {
            _followOffset = followOffset;
            _objectToFollow = objectToFollow;

            _startYPosition = objectToFollow.position.y;
        }

        private void FixedUpdate() =>
            transform.position = new Vector3(_objectToFollow.position.x, _startYPosition, _objectToFollow.position.z)+ _followOffset;
    }
}