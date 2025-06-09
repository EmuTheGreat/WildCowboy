using UnityEngine;
using UnityEngine.EventSystems;

public class HoldButtonSpawnTarget : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [Tooltip("������� ������ ����� ���������� ������")]
    public float requiredHoldTime = 2f;

    [Tooltip("������ ������")]
    public GameObject targetPrefab;

    [Tooltip("�����, � ������� �������� ������")]
    public Transform spawnPoint;

    [Tooltip("������ ���� ���������, ������� ���������� ����� ��������� ������")]
    public int tutorialStepIndex = 0;

    private float holdTimer;
    private bool isHolding;
    private bool targetSpawned;

    private TutorialManager tutorial;

    private void Awake()
    {
        tutorial = TutorialManager.Instance;  // ������������, ��� �� ������� ��� ����������
    }

    private void Update()
    {
        // ���� ������ ������ � ��� �� ���������� ������
        if (isHolding && !targetSpawned)
        {
            holdTimer += Time.deltaTime;
            if (holdTimer >= requiredHoldTime)
            {
                SpawnTarget();
                targetSpawned = true;
                tutorial.CompleteStep(tutorialStepIndex);
            }
        }
    }

    // ������� �� IPointerDown/Up (����� EventSystem �� �����)
    public void OnPointerDown(PointerEventData eventData)
    {
        // �������� ������, �� ������ ���� ������ ������� ������ ���
        if (tutorial.CurrentStepIndex == tutorialStepIndex)
        {
            isHolding = true;
            holdTimer = 0f;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // �����, ���� ��������� ������ �������
        isHolding = false;
        holdTimer = 0f;
    }

    private void SpawnTarget()
    {
        Instantiate(targetPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
