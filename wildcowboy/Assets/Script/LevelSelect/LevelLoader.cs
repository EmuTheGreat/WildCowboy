using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    private const string LevelKeyPrefix = "Level_";
    public int levelSceneNumber;
    public void LoadLevel()
    {
        if(ProgressManager.IsLevelUnlocked(levelSceneNumber))SceneManager.LoadScene(LevelKeyPrefix + levelSceneNumber);
    }
}
