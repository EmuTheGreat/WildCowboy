using UnityEngine;
using UnityEngine.UI;

public class TutorialUI : MonoBehaviour
{
    public Text hintText;
    public GameObject pointer; // UI-������� ��� �����

    public void ShowHint(string text, Transform target)
    {
        hintText.text = text;
        pointer.SetActive(true);
        // ������������� pointer �� target.position + ������
    }

    public void HideHint()
    {
        pointer.SetActive(false);
    }

    public void FinishTutorial()
    {
        // ������ ���� UI, ��������� ��������
        gameObject.SetActive(false);
    }
}
