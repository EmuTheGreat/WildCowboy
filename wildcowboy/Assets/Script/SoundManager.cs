using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public Button muteButton;  // Кнопка отключения звука
    public Image buttonImage;  // Картинка на кнопке
    public Sprite soundOnSprite;  // Иконка включенного звука
    public Sprite soundOffSprite; // Иконка выключенного звука

    private bool isMuted = false;

    void Start()
    {
        // Загружаем настройки звука
        isMuted = PlayerPrefs.GetInt("Muted", 0) == 1;
        AudioListener.volume = isMuted ? 0f : 1f;
        UpdateButtonImage();

        // Добавляем слушатель событий на кнопку
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
