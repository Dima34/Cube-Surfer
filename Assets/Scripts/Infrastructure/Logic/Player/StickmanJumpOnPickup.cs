using UnityEngine;

namespace Infrastructure.Logic.Player
{
    public class StickmanJumpOnPickup : MonoBehaviour
    {
        private const string JUMP_TRIGGER_NAME = "Jump";
        [SerializeField] private CubeHolder _cubeHolder;
        [SerializeField] private Animator _stickmanAnimator;

        private void Start() =>
            _cubeHolder.OnCubeAdded += PlayeJump;

        private void OnDestroy() =>
            _cubeHolder.OnCubeAdded += PlayeJump;

        private void PlayeJump() =>
            _stickmanAnimator.SetTrigger(JUMP_TRIGGER_NAME);
    }
}
