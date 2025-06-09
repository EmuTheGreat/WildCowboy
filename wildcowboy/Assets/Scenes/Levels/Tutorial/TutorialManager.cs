using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��������� ������������������� ����� ���������.
/// ���������� ��� Singleton ��� �������� �������.
/// </summary>
public class TutorialManager : MonoBehaviour
{
    // Singleton instance
    public static TutorialManager Instance { get; private set; }

    [Header("Steps Configuration")]
    [Tooltip("������ ����� ��������� � ������� ����������.")]
    [SerializeField]
    private List<TutorialStep> steps = new List<TutorialStep>();

    [Header("UI Reference")]
    [Tooltip("������ �� ���������, ���������� �� ����������� ���������.")]
    [SerializeField]
    private TutorialUI ui;

    /// <summary>
    /// ������� ������ ������������ ���� (��� ������� ��������).
    /// </summary>
    public int CurrentStepIndex { get; private set; } = 0;

    private void Awake()
    {
        // ���������� Singleton
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
    /// ���������������� ���������� ���� �����.
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
    /// �������� ��� ��� ����������� �����.
    /// </summary>
    public void CompleteStep(int index)
    {
        if (index >= 0 && index < steps.Count)
        {
            steps[index].IsCompleted = true;
        }
    }
}

