using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// ������� ��������: ��������� ������-������ � ��������� ������� ������ ��� ���������� ���� ��� ��������� �������.
/// ������ ������������ � UI Button � ������� ��������� � Text.
/// </summary>
public class SimpleTutorial : MonoBehaviour
{
    [Header("UI Elements")]
    [Tooltip("Button-������, �� ������� ����� ������ � ����������")]
    public Button holsterButton;
    [Tooltip("����� ���������")]
    public Text tutorialText;

    [Header("Existing Target")]
    [Tooltip("UI-������, ���������� ����������� �� �����")]
    public RectTransform targetUI;

    [Header("Hold Settings")]
    [Tooltip("����� ��������� � ��������")]
    public float holdTimeRequired = 3f;

    private enum State { WaitingHold, HoverTarget, Done }
    private State currentState = State.WaitingHold;

    private float holdTimer;
    private bool isPointerDown;

    void Awake()
    {
        // ������������ ���� ����������
        if (targetUI != null)
            targetUI.gameObject.SetActive(false);

        // ������������� �� ������� ������ ��� ���������
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
        ShowHint("������ ������ �� ������ � ����� ��� ��� �������� �� 3 �������");
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
                ShowHint("������ ������ �� ������");
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
        ShowHint("�������� ��������");
        currentState = State.Done;
    }

    private void ShowHint(string text)
    {
        if (tutorialText != null)
            tutorialText.text = text;
    }
}
