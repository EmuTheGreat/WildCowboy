using UnityEngine;
using UnityEngine.EventSystems;

public class HoldButtonSpawnTarget : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [Tooltip("Сколько секунд нужно удерживать кнопку")]
    public float requiredHoldTime = 2f;

    [Tooltip("Префаб мишени")]
    public GameObject targetPrefab;

    [Tooltip("Точка, в которой спавнить мишень")]
    public Transform spawnPoint;

    [Tooltip("Индекс шага туториала, который завершится после появления мишени")]
    public int tutorialStepIndex = 0;

    private float holdTimer;
    private bool isHolding;
    private bool targetSpawned;

    private TutorialManager tutorial;

    private void Awake()
    {
        tutorial = TutorialManager.Instance;  // предполагаем, что вы сделали его синглтоном
    }

    private void Update()
    {
        // Пока держим кнопку и ещё не заспавнили мишень
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

    // События от IPointerDown/Up (нужен EventSystem на сцене)
    public void OnPointerDown(PointerEventData eventData)
    {
        // Начинаем отсчёт, но только если сейчас активен нужный шаг
        if (tutorial.CurrentStepIndex == tutorialStepIndex)
        {
            isHolding = true;
            holdTimer = 0f;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // Сброс, если отпустили раньше времени
        isHolding = false;
        holdTimer = 0f;
    }

    private void SpawnTarget()
    {
        Instantiate(targetPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
