namespace Infrastructure.Services.SceneLoad
{
    public interface ISceneLoadService
    {
        void LoadScene(string sceneName);
        string GetCurrentLevel();
    }
}