using Infrastructure.Constants;
using Infrastructure.Services.SceneLoad;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class GameBootstrapper : MonoBehaviour
    {
        private ISceneLoadService _sceneLoadService;

        [Inject]
        public void Construct(ISceneLoadService sceneLoadService) =>
            _sceneLoadService = sceneLoadService;

        public void Awake() =>
            LoadGame();

        private void LoadGame() =>
            _sceneLoadService.LoadScene(Scenes.GAME);
    }
}