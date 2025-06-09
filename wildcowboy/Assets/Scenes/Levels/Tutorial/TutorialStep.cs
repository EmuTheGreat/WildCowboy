using UnityEngine.Events;
using UnityEngine;

[System.Serializable]
public class TutorialStep
{
    public string text;                        // ����� ���������
    public Transform highlightTransform;       // ���� ���������
    public UnityEvent onStepStart;             // (�����������) ������� ��� ������ ����
    public UnityEvent onStepComplete;          // ����� ������� ������ ����� ���������

    [HideInInspector] public bool IsCompleted; // �������� � true �����
}