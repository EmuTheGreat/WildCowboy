using UnityEngine;

public class SmoothShrink : MonoBehaviour
{
    public Vector3 targetScale = Vector3.zero; // �� ������ ������� ���������
    public float shrinkSpeed = 1f; // �������� ����������

    private void Update()
    {
        // ������� ���������� �������� �������
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * shrinkSpeed);

        // ����� ����������, ����� ����� �������� ����
        if (Vector3.Distance(transform.localScale, targetScale) < 0.01f)
        {
            transform.localScale = targetScale;
            enabled = false; // ��������� ������, ����� �� ��������� ��������
        }
    }
}
