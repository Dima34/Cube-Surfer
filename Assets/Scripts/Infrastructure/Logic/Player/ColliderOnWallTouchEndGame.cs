using System;
using Infrastructure.Constants;
using Infrastructure.Services.Death;
using UnityEngine;
using Zenject;

namespace Infrastructure.Logic.Player
{
    [RequireComponent(typeof(BoxCollider))]
    public class ColliderOnWallTouchEndGame : MonoBehaviour
    {
        private IDeathService _deathService;

        [Inject]
        public void Construct(IDeathService deathService) =>
            _deathService = deathService;

        private void OnCollisionEnter(Collision collision)
        {
            if (CollisionIsWallBlock(collision))
                EndGame();
        }

        private void EndGame()
        {
            _deathService.Die();
        }

        private static bool CollisionIsWallBlock(Collision collision) =>
            collision.gameObject.tag == Tags.WALL_CUBE;
    }
}