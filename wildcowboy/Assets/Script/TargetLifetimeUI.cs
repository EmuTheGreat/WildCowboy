using UnityEngine;
using UnityEngine.UI;

public class TargetLifetimeUI : MonoBehaviour
{
    [SerializeField] private Image fillImage;

    private float totalLifetime;
    private float timeLeft;
    private bool isRunning;

    public void Init(float lifetime)
    {
        totalLifetime = lifetime;
        timeLeft = lifetime;
        fillImage.fillAmount = 1f;
        isRunning = true;
    }

    void Update()
    {
        if (!isRunning) return;
        timeLeft -= Time.deltaTime;
        fillImage.fillAmount = Mathf.Clamp01(timeLeft / totalLifetime);
        if (timeLeft <= 0f)
            isRunning = false;
    }
}
