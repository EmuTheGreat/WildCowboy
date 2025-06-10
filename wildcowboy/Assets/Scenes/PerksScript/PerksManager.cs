using UnityEngine;
using UnityEngine.UI;

public class PerksManager : MonoBehaviour
{
    public Text TextReaction;
    public Text TextArmor;
    public Text TextAccuracy;
    public Text TextExp;

    public void Start()
    {
        UpdateText();
    }

    public void UpdateText()
    {
        var reactionLevel = PlayerPrefs.GetInt("Perk_Reaction");
        var armourLevel = PlayerPrefs.GetInt("Perk_Armor");
        var accuracyLevel = PlayerPrefs.GetInt("Perk_Accuracy");
        var expPoint = PlayerPrefs.GetInt("Exp_Point");
        TextReaction.text = $"{reactionLevel}";
        TextArmor.text = $"{armourLevel}";
        TextAccuracy.text = $"{accuracyLevel}";
        TextExp.text = $"{expPoint}";
    }
}
