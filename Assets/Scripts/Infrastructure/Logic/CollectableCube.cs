using System;
using System.Security.Cryptography;
using Infrastructure.Constants;
using Infrastructure.Logic.Player;
using UnityEngine;
using Zenject;

namespace Infrastructure.Logic
{
    [RequireComponent(typeof(BoxCollider))]
    public class CollectableCube : MonoBehaviour
    {
        private CubeHolder _cubeHolder;

        [Inject]
        public void Construct(CubeHolder cubeHolder) =>
            _cubeHolder = cubeHolder;

        private void OnTriggerEnter(Collider collidedObj)
        {
            if (collidedObj.gameObject.tag == Tags.COLLECTED_CUBE)
            {
                _cubeHolder.AddCube();
                Destroy(gameObject);
            }
        }
    }
}