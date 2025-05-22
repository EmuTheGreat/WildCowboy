using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBarDisplay : MonoBehaviour
{
    [SerializeField] private Image fillImage;
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private int maxHealth = 100;

    public void UpdateDisplay(int currentHealth)
    {
        float fillAmount = (float)currentHealth / maxHealth;
        fillImage.fillAmount = fillAmount;
        hpText.text = $"{currentHealth} / {maxHealth}";
    }

    public void SetMaxHealth(int max)
    {
        maxHealth = max;
    }
}