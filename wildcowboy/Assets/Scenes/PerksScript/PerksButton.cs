using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerksButton : MonoBehaviour
{
    public PerksManager Manager;

    public void UpgradePerk(string perkName) => Manager.UpgradePerk(perkName);
}
