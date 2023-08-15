using UnityEngine.SceneManagement;

namespace Infrastructure.Services.SceneLoad
{
    public class SceneLoadService : ISceneLoadService
    {
        private string _currentLevel;

        public string GetCurrentLevel() => _currentLevel;
        
        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
            _currentLevel = sceneName;
        }

    }
}