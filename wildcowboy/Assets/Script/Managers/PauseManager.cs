using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenu; // ������ �� ������ �����
    public Button pauseButton;   // ������ �����
    public Button resumeButton;  // ������ �������������
    public Button quitButton;    // ������ ������
    private bool isPaused = false;

    void Start()
    {
        // ����������� ������ � �������
        pauseButton.onClick.AddListener(TogglePause);
        resumeButton.onClick.AddListener(TogglePause);
        quitButton.onClick.AddListener(QuitGame);

        pauseMenu.transform.SetAsLastSibling();

        // ��������� ���� ����� ��� ������
        pauseMenu.SetActive(false);
    }

    void TogglePause()
    {
        isPaused = !isPaused;
        pauseMenu.SetActive(isPaused);
        Time.timeScale = isPaused ? 0f : 1f;

        // ���������� ���� �� �������� ����
        
    }

    void QuitGame()
    {
        Time.timeScale = 1f; // ���������� ���������� ����� ����� �������
        SceneManager.LoadScene("SelectLevel"); // �������� "MainMenu" �� ���� �����
    }
}
