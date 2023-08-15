using System;
using Infrastructure.Services.Input;
using UnityEngine;
using Zenject;

namespace Infrastructure.Logic.UI
{
    public class WaitForStartHUD : MonoBehaviour 
    {
        private IInputService _inputService;
        private bool _startTapped = false;
        public event Action OnHold;

        [Inject]
        public void Construct(IInputService inputService) =>
            _inputService = inputService;

        private void Update()
        {
            if (_inputService.OnStartTap && !_startTapped)
            {
                _startTapped = true;
                OnHold?.Invoke();
            }
        }
    }
}