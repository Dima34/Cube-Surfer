using System.Collections;
using UnityEngine;

namespace Infrastructure.Services.CoroutineRunner
{
    class CoroutineRunnerService : MonoBehaviour, ICoroutineRunnerService
    {
        public Coroutine Run(IEnumerator routine) =>
            StartCoroutine(routine);

        public void Stop(Coroutine coroutine) =>
            StopCoroutine(coroutine);
    }
}