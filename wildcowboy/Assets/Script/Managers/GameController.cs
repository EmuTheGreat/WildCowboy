using System;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int EnemyHealth;
    public int EnemyDamage;
    [SerializeField] private float TargetLifeTime;
    [SerializeField] private float TargetSize;
    public int ExperiencePoints;
    public GameObject VictoryBox;
    public GameObject LossBox;


    [SerializeField] private TargetManager targetManager;

    public GameObject hpEnemy;
    public GameObject hpSelf;
    [SerializeField] private HealthBarDisplay hpEnemyDisplay;
    [SerializeField] private HealthBarDisplay hpSelfDisplay;

    public int hpSelfCount;

    public bool enemyStep = false;
    public int[] unlockLevels;


    public TMP_Text victoryText;



    private void Awake()
    {
        //DebugPerks();

        if (VictoryBox != null) VictoryBox.SetActive(false);
        if(LossBox != null) LossBox.SetActive(false);

        Debug.Log(hpEnemy.name);
        hpEnemyDisplay = hpEnemy.GetComponent<HealthBarDisplay>();
        Debug.Log(hpEnemyDisplay is not null);
        hpSelfDisplay = hpSelf.GetComponent<HealthBarDisplay>();

        Debug.Log(PlayerPrefs.HasKey("PlayerHealth") ? "Ключ есть" : "Ключа нет");
        hpSelfCount = PlayerPrefs.HasKey("PlayerHealth") ? PlayerPrefs.GetInt("PlayerHealth") : 100;

        hpEnemyDisplay.SetMaxHealth(EnemyHealth);
        hpSelfDisplay.SetMaxHealth(hpSelfCount);

        UpdateHealthDisplays();
    }

    private void DebugPerks()
    {
        PlayerPrefs.SetInt("Perk_Reaction", 0);
        PlayerPrefs.SetInt("Perk_Armor", 0);
        PlayerPrefs.SetInt("Perk_Accuracy", 10);

        Debug.Log("Perk_Reaction" + PlayerPrefs.GetInt("Perk_Reaction"));
        Debug.Log("Perk_Armor" + PlayerPrefs.GetInt("Perk_Armor"));
        Debug.Log("Perk_Accuracy" + PlayerPrefs.GetInt("Perk_Accuracy"));
        //PlayerPrefs.SetInt("Exp_Point", 100);
    }

    private void FixedUpdate()
    {
        Debug.Log(EnemyHealth);
        if (EnemyHealth <= 0)
        {
            foreach (var levelNumber in unlockLevels)
            {
                ProgressManager.UnlockLevel(levelNumber);
            }

            var exp = PlayerPrefs.GetInt("Exp_Point");
            PlayerPrefs.SetInt("Exp_Point", exp + ExperiencePoints);

            victoryText.text = $"+{ExperiencePoints} очков опыта";
            VictoryBox.SetActive(true);
        }

        if (hpSelfCount <= 0)
        {
            LossBox.SetActive(true);
        }
    }

    public void DamageEnemy(int amount)
    {
        EnemyHealth = Mathf.Max(EnemyHealth - (amount + 5 * PlayerPrefs.GetInt("Perk_Reaction")), 0);
        hpEnemyDisplay.UpdateDisplay(EnemyHealth);
    }

    public void DamageSelf(int amount)
    {
        var damage = Math.Abs(amount - 3 * PlayerPrefs.GetInt("Perk_Armor"));
        hpSelfCount = Mathf.Max(hpSelfCount - damage, 0);
        hpSelfDisplay.UpdateDisplay(hpSelfCount);
    }

    public void HealEnemy(int amount)
    {
        EnemyHealth = Mathf.Min(EnemyHealth + amount, 100);
        hpEnemyDisplay.UpdateDisplay(EnemyHealth);
    }

    public void HealSelf(int amount)
    {
        hpSelfCount = Mathf.Min(hpSelfCount + amount, 100);
        hpSelfDisplay.UpdateDisplay(hpSelfCount);
    }

    public void UpdateHealthDisplays()
    {
        hpEnemyDisplay.UpdateDisplay(EnemyHealth);
        hpSelfDisplay.UpdateDisplay(hpSelfCount);
    }
}
