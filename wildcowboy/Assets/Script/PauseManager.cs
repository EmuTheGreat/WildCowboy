using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenu; // Ссылка на панель паузы
    public Button pauseButton;   // Кнопка паузы
    public Button resumeButton;  // Кнопка возобновления
    public Button quitButton;    // Кнопка выхода
    private bool isPaused = false;

    void Start()
    {
        // Привязываем кнопки к методам
        pauseButton.onClick.AddListener(TogglePause);
        resumeButton.onClick.AddListener(TogglePause);
        quitButton.onClick.AddListener(QuitGame);

        pauseMenu.transform.SetAsLastSibling();

        // Отключаем меню паузы при старте
        pauseMenu.SetActive(false);
    }

    void TogglePause()
    {
        isPaused = !isPaused;
        pauseMenu.SetActive(isPaused);
        Time.timeScale = isPaused ? 0f : 1f;

        // Перемещаем меню на передний план
        
    }

    void QuitGame()
    {
        Time.timeScale = 1f; // Возвращаем нормальное время перед выходом
        SceneManager.LoadScene("SelectLevel"); // Замените "MainMenu" на свою сцену
    }
}
