using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject menuPanel;  // ������ �� ������ ����
    public Button menuButton;      // ������ �������� ����
    public Button closeButton;     // ������ �������� ����

    private bool isMenuOpen = false;

    void Start()
    {
        // ��������� ���� ��� ������
        menuPanel.SetActive(false);

        // ����������� ������ � �������
        menuButton.onClick.AddListener(ToggleMenu);
        closeButton.onClick.AddListener(ToggleMenu);
    }

    void ToggleMenu()
    {
        isMenuOpen = !isMenuOpen;
        menuPanel.SetActive(isMenuOpen);
    }
}
