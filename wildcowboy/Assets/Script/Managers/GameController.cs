using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public int EnemyHealth;
    public int EnemyDamage;
    [SerializeField] private float TargetLifeTime;
    [SerializeField] private float TargetSize;



    [SerializeField] private TargetManager targetManager;

    public GameObject hpEnemy;
    public GameObject hpSelf;
    [SerializeField] private HealthBarDisplay hpEnemyDisplay;
    [SerializeField] private HealthBarDisplay hpSelfDisplay;

    public int hpSelfCount;

    public bool enemyStep = false;
    public int[] unlockLevels;

    private void Awake()
    {
        Debug.Log(hpEnemy.name);
        hpEnemyDisplay = hpEnemy.GetComponent<HealthBarDisplay>();
        Debug.Log(hpEnemyDisplay is not null);
        hpSelfDisplay = hpSelf.GetComponent<HealthBarDisplay>();

        Debug.Log(PlayerPrefs.HasKey("PlayerHealth") ? "Ключ есть": "Ключа нет");
        hpSelfCount = PlayerPrefs.HasKey("PlayerHealth") ? PlayerPrefs.GetInt("PlayerHealth") : 100;

        hpEnemyDisplay.SetMaxHealth(EnemyHealth);
        hpSelfDisplay.SetMaxHealth(hpSelfCount);

        UpdateHealthDisplays();
    }

    private void FixedUpdate()
    {
        if (EnemyHealth <= 0 || hpSelfCount <= 0)
        {
            foreach (var levelNumber in unlockLevels)
            {
                ProgressManager.UnlockLevel(levelNumber);
            }

            SceneManager.LoadScene("SelectLevel");
        }
    }

    public void DamageEnemy(int amount)
    {
        EnemyHealth = Mathf.Max(EnemyHealth - amount, 0);
        hpEnemyDisplay.UpdateDisplay(EnemyHealth);
    }

    public void DamageSelf(int amount)
    {
        hpSelfCount = Mathf.Max(hpSelfCount - amount, 0);
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
