using System;
using Infrastructure.Constants;
using UnityEngine;

namespace Infrastructure.Logic.Player
{
    [RequireComponent(typeof(BoxCollider))]
    public class ColliderOnWallTouchEndGame : MonoBehaviour
    {
        public event Action OnGameEnd;
        
        private void OnCollisionEnter(Collision collision)
        {
            if (CollisionIsWallBlock(collision))
                OnGameEnd?.Invoke();
        }
        
        private static bool CollisionIsWallBlock(Collision collision) =>
            collision.gameObject.tag == Tags.WALL_CUBE;
    }
}