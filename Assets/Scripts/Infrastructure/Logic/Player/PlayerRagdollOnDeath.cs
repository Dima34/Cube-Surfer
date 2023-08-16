using System;
using Infrastructure.Services.Death;
using UnityEngine;
using Zenject;

namespace Infrastructure.Logic.Player
{
    public class PlayerRagdollOnDeath : MonoBehaviour
    {
        [SerializeField] private GameObject _playerRagdoll;
        [SerializeField] private Animator _stickmanAnimator;
        [SerializeField] private Rigidbody _stickmanRB;

        private IDeathService _deathService;
        
        private const int PUSH_FORCE = 10;
        
        [Inject]
        public void Construct(IDeathService deathService) =>
            _deathService = deathService;

        private void Start() =>
            _deathService.Happend += MakeRagdollFromPlayer;

        private void OnDestroy() =>
            _deathService.Happend -= MakeRagdollFromPlayer;

        private void MakeRagdollFromPlayer()
        {
            EnableRagdoll();
            DisableAnimator();
            PushPlayer();
        }

        private void EnableRagdoll() =>
            _playerRagdoll.SetActive(true);

        private void DisableAnimator() =>
            _stickmanAnimator.enabled = false;

        private void PushPlayer() =>
            _stickmanRB.AddForce(Vector3.forward * PUSH_FORCE, ForceMode.Impulse);
    }
}