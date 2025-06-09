using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// ��������������� ������: �������� � ������� ������ ��������� ������-������,
/// ������ � ��������� �� ������.
/// </summary>
public class TutorialHoldButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [Header("��������� �������� ��������")]
    public float allowedMovementRadius = 50f;
    public Color gizmoColor = new Color(1, 0.5f, 0, 0.3f);

    [Header("������ �� ����������")]
    public TextMeshProUGUI feedbackText;
    public TargetManager targetManager;
    public GameController gameController;
    public GameObject targetPrefab;
    public Button selectLevel;

    [Header("UI ��� ���������")]
    public TextMeshProUGUI tutorialText;

    [Header("��������� ���������")]
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
        // ������ ���� � �������� ������
        targetManager.HideTarget();
        ClearFeedback();
        ShowTutorialHint("������ ������ �� ������, ����� ��� � �� ������ ������ 3 �������");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // �������� ��������� ������ � ������ ��������� � ���� �� ��������� ���
        if (currentState == TutorialState.Hold && !gameController.enemyStep)
        {
            isHolding = true;
            initialPressPosition = eventData.position;
            holdTimer = 0f;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // ��� ���������� � ���� ��������� ��������� ���������
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
            // �����, ���� �������� ������ �������
            holdTimer = 0f;
        }
    }

    void Update()
    {
        if (currentState == TutorialState.Hold && isHolding)
        {
            holdTimer += Time.deltaTime;

            // ���� ������ ���� �� ������� �������, ��������������� ��������
            if (Vector2.Distance(initialPressPosition, Input.mousePosition) > allowedMovementRadius)
            {
                feedbackText.text = "TOO EARLY!";
                feedbackText.color = Color.red;
                Invoke("ClearFeedback", 1.5f);
                isHolding = false;
                return;
            }

            // ��������� ��������� ������� �������
            if (holdTimer >= tutorialHoldTime)
            {
                // ���������� ������ � ��������� � ���������� ����
                targetPrefab.SetActive(true);
                targetManager.canSpawn = true;
                targetManager.Spawn();
                currentState = TutorialState.Hover;
                ShowTutorialHint("������� ������ ������ �� ������ � �������");
                isHolding = false;
            }
        }
    }

    private void CompleteTutorial()
    {
        // ��������� �������� � ��������� gameController � ��������� ���
        selectLevel.gameObject.SetActive(true);
        targetPrefab.SetActive(false);
        ShowTutorialHint("�������� ��������");
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
        // ���������� ������ �������� � �����
        if (currentState == TutorialState.Hold)
        {
            Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(
                new Vector3(initialPressPosition.x, initialPressPosition.y, Camera.main.nearClipPlane));
            Gizmos.color = gizmoColor;
            Gizmos.DrawSphere(mouseWorld, allowedMovementRadius * 0.01f);
        }
    }
}