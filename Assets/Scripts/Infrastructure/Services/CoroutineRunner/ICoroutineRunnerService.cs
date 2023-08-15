using System.Collections;
using UnityEngine;

namespace Infrastructure.Services.CoroutineRunner
{
    public interface ICoroutineRunnerService
    {
        Coroutine Run(IEnumerator routine);
        void Stop(Coroutine coroutine);
    }
}