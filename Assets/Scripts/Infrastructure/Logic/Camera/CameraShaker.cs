using System;
using System.Collections;
using Cinemachine;
using Infrastructure.Services.CoroutineRunner;
using UnityEngine;
using Zenject;

namespace Infrastructure.Logic
{
    public class CameraShaker : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _cinemachineVirtualCamera;
        [SerializeField] private float _shakeFrequency;
        [SerializeField] private float _timeToShake;
        private ICoroutineRunnerService _coroutineRunnerService;
        private CinemachineBasicMultiChannelPerlin _cinemachinePerlin;
        private Coroutine _shakingProcess;

        [Inject]
        public void Construct(ICoroutineRunnerService coroutineRunnerService) =>
            _coroutineRunnerService = coroutineRunnerService;

        private void Start()
        {
            _cinemachinePerlin =
                _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }

        private void OnDestroy()
        {
            if(_shakingProcess != null)
                _coroutineRunnerService.Stop(_shakingProcess);
        }

        public void Shake()
        {
            if (_shakingProcess == null)
                _shakingProcess = _coroutineRunnerService.Run(ShakeProcess());
        }

        private IEnumerator ShakeProcess()
        {
            _cinemachinePerlin.m_FrequencyGain = _shakeFrequency;
            yield return new WaitForSecondsRealtime(_timeToShake);
            _cinemachinePerlin.m_FrequencyGain = 0;

            _shakingProcess = null;
        }
    }
}