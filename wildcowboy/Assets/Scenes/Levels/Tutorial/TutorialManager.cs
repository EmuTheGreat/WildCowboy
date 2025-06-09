using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Управляет последовательностью шагов туториала.
/// Реализован как Singleton для удобного доступа.
/// </summary>
public class TutorialManager : MonoBehaviour
{
    // Singleton instance
    public static TutorialManager Instance { get; private set; }

    [Header("Steps Configuration")]
    [Tooltip("Список шагов туториала в порядке выполнения.")]
    [SerializeField]
    private List<TutorialStep> steps = new List<TutorialStep>();

    [Header("UI Reference")]
    [Tooltip("Ссылка на компонент, отвечающий за отображение подсказок.")]
    [SerializeField]
    private TutorialUI ui;

    /// <summary>
    /// Текущий индекс выполняемого шага (для внешней проверки).
    /// </summary>
    public int CurrentStepIndex { get; private set; } = 0;

    private void Awake()
    {
        // Реализация Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        StartCoroutine(RunTutorial());
    }

    /// <summary>
    /// Последовательное выполнение всех шагов.
    /// </summary>
    private IEnumerator RunTutorial()
    {
        for (int i = 0; i < steps.Count; i++)
        {
            CurrentStepIndex = i;
            TutorialStep step = steps[i];
            step.IsCompleted = false;
            step.onStepStart?.Invoke();

            ui.ShowHint(step.text, step.highlightTransform);
            yield return new WaitUntil(() => step.IsCompleted);

            ui.HideHint();
            step.onStepComplete?.Invoke();
        }

        CurrentStepIndex = -1;
        ui.FinishTutorial();
    }

    /// <summary>
    /// Отметить шаг как завершённый извне.
    /// </summary>
    public void CompleteStep(int index)
    {
        if (index >= 0 && index < steps.Count)
        {
            steps[index].IsCompleted = true;
        }
    }
}

