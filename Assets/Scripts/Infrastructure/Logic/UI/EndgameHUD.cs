using System;
using UnityEngine;
using UnityEngine.UI;

namespace Infrastructure.Logic.UI
{
    public class EndgameHUD : MonoBehaviour
    {
        [SerializeField] private Button _tryAgainButtin;
        
        public event Action OnTryAgainClick;

        private void Awake() =>
            _tryAgainButtin.onClick.AddListener(FireTryAgainClick);

        private void FireTryAgainClick() =>
            OnTryAgainClick?.Invoke();
    }
}