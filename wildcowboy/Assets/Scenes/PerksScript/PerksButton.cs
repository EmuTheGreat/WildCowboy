using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerksButton : MonoBehaviour
{
    public void UpgradePerkAccuracy(string perkName)
    {
        var expPoint = PlayerPrefs.GetInt("Exp_Point");
        if (expPoint >= 100)
        {
            var perkLevel = PlayerPrefs.GetInt(perkName);
            PlayerPrefs.SetInt(perkName, perkLevel++);
        }
        PlayerPrefs.SetInt("Exp_Point", expPoint - 100);
    }
}
