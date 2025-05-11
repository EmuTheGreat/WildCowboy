using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour
{
    public int levelNumber;
    public string levelSceneName;
    public Button button;
    public GameObject[] lockIcon;

    void Start()
    {
        bool unlocked = ProgressManager.IsLevelUnlocked(levelNumber);
        button.interactable = unlocked;
        foreach (var icon in lockIcon)
        {
            icon.SetActive(!unlocked);
        }
        if (unlocked)
            button.onClick.AddListener(() => SceneManager.LoadScene(levelSceneName));
    }
}
