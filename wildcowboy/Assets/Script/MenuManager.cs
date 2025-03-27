using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject menuPanel;  // Ссылка на панель меню
    public Button menuButton;      // Кнопка открытия меню
    public Button closeButton;     // Кнопка закрытия меню

    private bool isMenuOpen = false;

    void Start()
    {
        // Отключаем меню при старте
        menuPanel.SetActive(false);

        // Привязываем кнопки к методам
        menuButton.onClick.AddListener(ToggleMenu);
        closeButton.onClick.AddListener(ToggleMenu);
    }

    void ToggleMenu()
    {
        isMenuOpen = !isMenuOpen;
        menuPanel.SetActive(isMenuOpen);
    }
}
