using UnityEngine;

namespace Infrastructure.Logic.Player
{
    public class CubeStackEffect : MonoBehaviour
    {
        [SerializeField] private CubeHolder _cubeHolder;
        [SerializeField] private ParticleSystem _dustEffect;

        private void Start() =>
            _cubeHolder.OnCubeAdded += PlayEffect;

        private void OnDestroy() =>
            _cubeHolder.OnCubeAdded += PlayEffect;

        private void PlayEffect() =>
            _dustEffect.Play();
    }
}