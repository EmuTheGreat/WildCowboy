using UnityEngine;
using UnityEngine.UI;

public class TutorialUI : MonoBehaviour
{
    public Text hintText;
    public GameObject pointer; // UI-стрелка или рамка

    public void ShowHint(string text, Transform target)
    {
        hintText.text = text;
        pointer.SetActive(true);
        // позиционируем pointer на target.position + оффсет
    }

    public void HideHint()
    {
        pointer.SetActive(false);
    }

    public void FinishTutorial()
    {
        // пр€чем весь UI, сохран€ем прогресс
        gameObject.SetActive(false);
    }
}
