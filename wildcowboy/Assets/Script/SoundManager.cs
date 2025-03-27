using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public Button muteButton;  // ������ ���������� �����
    public Image buttonImage;  // �������� �� ������
    public Sprite soundOnSprite;  // ������ ����������� �����
    public Sprite soundOffSprite; // ������ ������������ �����

    private bool isMuted = false;

    void Start()
    {
        // ��������� ��������� �����
        isMuted = PlayerPrefs.GetInt("Muted", 0) == 1;
        AudioListener.volume = isMuted ? 0f : 1f;
        UpdateButtonImage();

        // ��������� ��������� ������� �� ������
        muteButton.onClick.AddListener(ToggleSound);
    }

    void ToggleSound()
    {
        isMuted = !isMuted;
        AudioListener.volume = isMuted ? 0f : 1f;
        PlayerPrefs.SetInt("Muted", isMuted ? 1 : 0);
        PlayerPrefs.Save();
        UpdateButtonImage();
    }

    void UpdateButtonImage()
    {
        buttonImage.sprite = isMuted ? soundOffSprite : soundOnSprite;
    }
}
