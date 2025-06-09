using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// Простой туториал: удержание кнопки-кобуры и активация готовой мишени для завершения шага при наведении курсора.
/// Скрипт подключается к UI Button и выводит подсказки в Text.
/// </summary>
public class SimpleTutorial : MonoBehaviour
{
    [Header("UI Elements")]
    [Tooltip("Button-кобура, на которую нужно нажать и удерживать")]
    public Button holsterButton;
    [Tooltip("Текст подсказок")]
    public Text tutorialText;

    [Header("Existing Target")]
    [Tooltip("UI-мишень, изначально отключенная на сцене")]
    public RectTransform targetUI;

    [Header("Hold Settings")]
    [Tooltip("Время удержания в секундах")]
    public float holdTimeRequired = 3f;

    private enum State { WaitingHold, HoverTarget, Done }
    private State currentState = State.WaitingHold;

    private float holdTimer;
    private bool isPointerDown;

    void Awake()
    {
        // Деактивируем цель изначально
        if (targetUI != null)
            targetUI.gameObject.SetActive(false);

        // Подписываемся на события кнопки для удержания
        var trigger = holsterButton.gameObject.GetComponent<EventTrigger>()
                      ?? holsterButton.gameObject.AddComponent<EventTrigger>();

        // OnPointerDown
        var entryDown = new EventTrigger.Entry { eventID = EventTriggerType.PointerDown };
        entryDown.callback.AddListener(_ => OnHolsterPress());
        trigger.triggers.Add(entryDown);

        // OnPointerUp
        var entryUp = new EventTrigger.Entry { eventID = EventTriggerType.PointerUp };
        entryUp.callback.AddListener(_ => OnHolsterRelease());
        trigger.triggers.Add(entryUp);
    }

    void Start()
    {
        ShowHint("Наведи курсор на кобуру и зажми ЛКМ без движения на 3 секунды");
    }

    void Update()
    {
        if (currentState == State.WaitingHold && isPointerDown)
        {
            holdTimer += Time.deltaTime;
            if (holdTimer >= holdTimeRequired)
            {
                ActivateTarget();
                currentState = State.HoverTarget;
                ShowHint("Наведи курсор на мишень");
            }
        }
        else if (currentState == State.HoverTarget)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(
                    targetUI,
                    Input.mousePosition,
                    null))
            {
                CompleteTutorial();
            }
        }
    }

    private void OnHolsterPress()
    {
        if (currentState == State.WaitingHold)
        {
            isPointerDown = true;
            holdTimer = 0f;
        }
    }

    private void OnHolsterRelease()
    {
        isPointerDown = false;
        if (currentState == State.WaitingHold)
            holdTimer = 0f;
    }

    private void ActivateTarget()
    {
        if (targetUI != null)
            targetUI.gameObject.SetActive(true);
    }

    private void CompleteTutorial()
    {
        ShowHint("Туториал завершён");
        currentState = State.Done;
    }

    private void ShowHint(string text)
    {
        if (tutorialText != null)
            tutorialText.text = text;
    }
}
