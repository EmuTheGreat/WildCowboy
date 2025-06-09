using UnityEngine.Events;
using UnityEngine;

[System.Serializable]
public class TutorialStep
{
    public string text;                        // “екст подсказки
    public Transform highlightTransform;       //  уда указывать
    public UnityEvent onStepStart;             // (опционально) событи€ при старте шага
    public UnityEvent onStepComplete;          // можно цепл€ть логику через инспектор

    [HideInInspector] public bool IsCompleted; // ставитс€ в true извне
}