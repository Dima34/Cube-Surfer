using System.Collections;
using Infrastructure.Services.CoroutineRunner;
using UnityEngine;
using Zenject;

namespace Infrastructure.Logic.Player
{
    public class CubePickupText : MonoBehaviour
    {
        [SerializeField] private float _lifeTime;

        private ICoroutineRunnerService _coroutineRunner;
        private LevelBuilder _levelBuilder;
        private Coroutine _destroyingCoroutine;

        [Inject]
        public void Construct(LevelBuilder levelBuilder, ICoroutineRunnerService coroutineRunner)
        {
            _levelBuilder = levelBuilder;
            _coroutineRunner = coroutineRunner;
        }

        private void Start()
        {
            _destroyingCoroutine = _coroutineRunner.Run(DestroyingProcess());
        }

        private IEnumerator DestroyingProcess()
        {
            yield return new WaitForSecondsRealtime(_lifeTime);
            _levelBuilder.DestroySpawnText(this);
            _destroyingCoroutine = null;
        }
        
        private void OnDestroy()
        {
            if (_destroyingCoroutine != null)
                _coroutineRunner.Stop(_destroyingCoroutine);
        }
    }
}