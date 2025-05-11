using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour
{
    public int levelNumber;
    public string levelSceneName;
    public Button button;

    void Start()
    {
        bool unlocked = ProgressManager.IsLevelUnlocked(levelNumber);

        button.interactable = unlocked;
        if (unlocked)
            button.onClick.AddListener(() => SceneManager.LoadScene(levelSceneName));

        for (int i = 0; i < 10; i++) // допустим, до 10 уровней
        {
            var unlockedLog = PlayerPrefs.GetInt("Level_" + i, 0);
            Debug.Log($"Level {i}: {(unlockedLog == 1 ? "Unlocked" : "Locked")}");
        }
    }

}
