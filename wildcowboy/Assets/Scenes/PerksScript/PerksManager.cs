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

    public void UpgradePerk(string perkName)
    {
        var expPoint = PlayerPrefs.GetInt("Exp_Point");
        if (expPoint - 100 >= 0)
        {
            var perkLevel = PlayerPrefs.GetInt(perkName);
            PlayerPrefs.SetInt(perkName, ++perkLevel);
            PlayerPrefs.SetInt("Exp_Point", expPoint - 100);
            UpdateText();
        }
    }
}
