using Infrastructure.Constants;
using Infrastructure.Logic.UI;
using UnityEngine;
using Zenject;

namespace Infrastructure.Factories.UI
{
    public class UIFactory : IUIFactory
    {
        private DiContainer _container;
        private RectTransform _canvasRect;
        private GameObject _waitingHud;
        private GameObject _endgameHud;

        public UIFactory(DiContainer container)
        {
            _container = container;

            CreateCanvas();
        }

        private void CreateCanvas()
        {
            GameObject canvas = _container.InstantiatePrefabResource(ResourcePaths.FACTORY_CANVAS);
            _canvasRect = canvas.GetComponent<RectTransform>();
        }

        public WaitForStartHUD SpawnStartWaitingHUD()
        {
            _waitingHud = _container.InstantiatePrefabResource(ResourcePaths.START_WAITING_HUD, _canvasRect);
            return _waitingHud.GetComponent<WaitForStartHUD>();
        }

        public void RemoveStartWaitingHUD() =>
            Object.Destroy(_waitingHud.gameObject);

        public EndgameHUD SpawnEndgameHUD()
        {
            _endgameHud = _container.InstantiatePrefabResource(ResourcePaths.ENDGAME_HUD, _canvasRect);
            return _endgameHud.GetComponent<EndgameHUD>();
        }

        public void RemoveEndgameHUD() =>
            Object.Destroy(_endgameHud.gameObject);
    }
}