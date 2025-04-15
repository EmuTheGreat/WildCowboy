using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoldButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [Header("Settings")]
    public float holdTime = 2f; // Время, после которого появляется мишень
    public float allowedMovementRadius = 50f;
    public Color gizmoColor = new Color(1, 0.5f, 0, 0.3f);

    [Header("References")]
    public TextMeshProUGUI feedbackText;
    public TargetManager targetManager;
    public GameController gameController;

    public bool isHolding = false;
    public bool isTooEarly = false;
    public bool isTargetActive = false; // Флаг, что мишень появилась
    private Vector2 initialPressPosition;
    private float holdTimer = 0f;

    public bool IsHolding => isHolding;
    public bool IsTooEarly => isTooEarly;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!gameController.enemyStep) {
            isHolding = true;
            isTooEarly = true;
            initialPressPosition = eventData.position;
            holdTimer = 0f;
            isTargetActive = false;
            targetManager.canSpawn = true;
            targetManager.Spawn();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {

        Debug.Log($"{targetManager.IsCursorOverTarget()}");
        if (isTargetActive && targetManager != null && targetManager.IsCursorOverTarget())
        {
            feedbackText.text = "HIT!";
            feedbackText.color = Color.green;
            targetManager.HideTarget();
        }
        else
        {
            feedbackText.text = "MISS!";
            feedbackText.color = Color.red;
        }

        Invoke("ClearFeedback", 1.5f);
        isHolding = false;
        isTargetActive = false;
    }

    void Update()
    {
        if (isHolding)
        {
            holdTimer += Time.deltaTime;

            if (isTooEarly && Vector2.Distance(initialPressPosition, Input.mousePosition) > allowedMovementRadius)
            {
                TooEarlyMovement();
            }
        }
    }

    public void TooEarlyMovement()
    {
        isTooEarly = true;
        isHolding = false;

        if (feedbackText != null)
        {
            feedbackText.text = "TOO EARLY!";
            feedbackText.color = Color.red;
            Invoke("ClearFeedback", 1.5f);
        }
    }

    private void ClearFeedback()
    {
        if (feedbackText != null)
            feedbackText.text = "";
    }
}
