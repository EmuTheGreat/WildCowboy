using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerksButton : MonoBehaviour
{
    public void UpgradePerkAccuracy(string perkName)
    {
        var perkLevel = PlayerPrefs.GetInt(perkName);
        PlayerPrefs.SetInt(perkName, perkLevel++);
    }
}
