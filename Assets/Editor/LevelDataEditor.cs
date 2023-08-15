using Infrastructure.Constants;
using StaticData;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[CustomEditor(typeof(LevelStaticData))]
public class LevelStaticDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        LevelStaticData levelData = (LevelStaticData)target;

        if (GUILayout.Button("Collect"))
        {
            AddSceneNameData(levelData);
            TryAddStartSectionData(levelData);
            TryAddPlayerStartPoint(levelData);

            EditorUtility.SetDirty(levelData);
        }
    }

    private static void AddSceneNameData(LevelStaticData levelData) =>
        levelData.LevelName = SceneManager.GetActiveScene().name;

    private static void TryAddStartSectionData(LevelStaticData levelData)
    {
        string levelSpawnPointTag = Tags.LEVEL_SPAWN_POINT;
        GameObject levelSpawnpPoint = GameObject.FindGameObjectWithTag(levelSpawnPointTag);

        if (levelSpawnpPoint != null)
            levelData.StartSectionPosition = levelSpawnpPoint.transform.position;
        else
            Debug.LogWarning($"Can`t find level spawn point. Mark object by '{levelSpawnPointTag}' tag");
    }

    private static void TryAddPlayerStartPoint(LevelStaticData levelData)
    {
        string respawnPointTag = Tags.PLAYER_SPAWN_POINT;
        GameObject respawnPoint = GameObject.FindGameObjectWithTag(respawnPointTag);
        
        if (respawnPoint != null)
            levelData.PlayerSpawnPoint = respawnPoint.transform.position;
        else
            Debug.LogWarning($"Can`t find player start position. Mark object by '{respawnPointTag}' tag");
    }
}