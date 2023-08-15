using System.Collections;
using Infrastructure.Constants;
using Infrastructure.Logic.Player;
using Infrastructure.Services.CoroutineRunner;
using UnityEngine;
using Zenject;

namespace Infrastructure.Logic
{
    public class CollectedCubeRemover : MonoBehaviour
    {
        private CubeHolder _cubeHolder;
        private bool _isRemovingProcess;
        private ICoroutineRunnerService _coroutineRunner;

        [Inject]
        public void Construct(CubeHolder cubeHolder, ICoroutineRunnerService coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
            _cubeHolder = cubeHolder;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (CollisionIsWallBlock(collision) && RemovingProccessNotRun())
                _coroutineRunner.Run(RemoveCubeProcess(collision));
        }

        private IEnumerator RemoveCubeProcess(Collision collision)
        {
            _isRemovingProcess = true;
            yield return null;
            
            _cubeHolder.RemoveCube(transform.gameObject);
            transform.SetParent(collision.transform);

            _isRemovingProcess = false;
        }

        private bool RemovingProccessNotRun() =>
            !_isRemovingProcess;

        private static bool CollisionIsWallBlock(Collision collision) =>
            collision.gameObject.tag == Tags.WALL_CUBE;
    }
}