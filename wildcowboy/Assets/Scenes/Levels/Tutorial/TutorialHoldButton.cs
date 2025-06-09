using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Комбинированный скрипт: туториал и игровая логика удержания кнопки-кобуры,
/// спавна и попадания по мишени.
/// </summary>
public class TutorialHoldButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [Header("Настройки игрового процесса")]
    public float allowedMovementRadius = 50f;
    public Color gizmoColor = new Color(1, 0.5f, 0, 0.3f);

    [Header("Ссылки на компоненты")]
    public TextMeshProUGUI feedbackText;
    public TargetManager targetManager;
    public GameController gameController;
    public GameObject targetPrefab;
    public Button selectLevel;

    [Header("UI для туториала")]
    public TextMeshProUGUI tutorialText;

    [Header("Настройки туториала")]
    public float tutorialHoldTime = 3f;

    private enum TutorialState { Hold, Hover, Done }
    private TutorialState currentState = TutorialState.Hold;

    public bool isHolding;
    private float holdTimer;
    private Vector2 initialPressPosition;

    void Start()
    {
        selectLevel.gameObject.SetActive(false);
        targetPrefab.SetActive(false);
        // Скрыть цель и сбросить фидбэк
        targetManager.HideTarget();
        ClearFeedback();
        ShowTutorialHint("Наведи курсор на кобуру, зажми ЛКМ и не двигай курсор 3 секунды");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Начинаем удержание только в начале туториала и если не вражеский шаг
        if (currentState == TutorialState.Hold && !gameController.enemyStep)
        {
            isHolding = true;
            initialPressPosition = eventData.position;
            holdTimer = 0f;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // При отпускании в фазе наведения проверяем попадание
        isHolding = false;
        if (currentState == TutorialState.Hover)
        {
            bool hit = targetManager.IsCursorOverTarget();
            feedbackText.text = hit ? "HIT!" : "MISS!";
            feedbackText.color = hit ? Color.green : Color.red;
            Invoke("ClearFeedback", 1.5f);
            CompleteTutorial();
        }
        else if (currentState == TutorialState.Hold)
        {
            // Сброс, если отпущено раньше времени
            holdTimer = 0f;
        }
    }

    void Update()
    {
        if (currentState == TutorialState.Hold && isHolding)
        {
            holdTimer += Time.deltaTime;

            // Если курсор ушёл за пределы радиуса, преждевременное движение
            if (Vector2.Distance(initialPressPosition, Input.mousePosition) > allowedMovementRadius)
            {
                feedbackText.text = "TOO EARLY!";
                feedbackText.color = Color.red;
                Invoke("ClearFeedback", 1.5f);
                isHolding = false;
                return;
            }

            // Дождались удержания нужного времени
            if (holdTimer >= tutorialHoldTime)
            {
                // Активируем мишень и переходим к следующему шагу
                targetPrefab.SetActive(true);
                targetManager.canSpawn = true;
                targetManager.Spawn();
                currentState = TutorialState.Hover;
                ShowTutorialHint("Зажатый курсор наведи на мишень и отпусти");
                isHolding = false;
            }
        }
    }

    private void CompleteTutorial()
    {
        // Завершаем туториал и переводим gameController в следующий шаг
        selectLevel.gameObject.SetActive(true);
        targetPrefab.SetActive(false);
        ShowTutorialHint("Туториал завершён");
        currentState = TutorialState.Done;
        gameController.enemyStep = true;


        foreach (var levelNumber in gameController.unlockLevels)
        {
            ProgressManager.UnlockLevel(levelNumber);
        }
    }

    private void ShowTutorialHint(string text)
    {
        if (tutorialText != null)
            tutorialText.text = text;
    }

    private void ClearFeedback()
    {
        if (feedbackText != null)
            feedbackText.text = string.Empty;
    }

    void OnDrawGizmosSelected()
    {
        // Показываем радиус движения в сцене
        if (currentState == TutorialState.Hold)
        {
            Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(
                new Vector3(initialPressPosition.x, initialPressPosition.y, Camera.main.nearClipPlane));
            Gizmos.color = gizmoColor;
            Gizmos.DrawSphere(mouseWorld, allowedMovementRadius * 0.01f);
        }
    }
}