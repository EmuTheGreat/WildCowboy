using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TargetManager : MonoBehaviour
{
    public GameObject targetPrefab; 
    public BoxCollider2D spawnArea; 
    private GameObject currentTarget;
    public HoldButton button;
    public bool canSpawn = false;
    public float targetLifetime = 5f;
    public GameObject hpEnemy;
    public EnemyTurnEffect enemyTurnEffect;
    public GameController controller;
    public AudioSource audioFireSource;


    public float minSpawnDelay = 1f;
    public float maxSpawnDelay = 3f;


    public void Spawn()
    {
        if (canSpawn)
        {
            StartCoroutine(SpawnTarget());
        }
    }

    IEnumerator SpawnTarget()
    {
        canSpawn = false;
        float delay = Random.Range(minSpawnDelay, maxSpawnDelay);
        yield return new WaitForSeconds(delay);

        if (button.isHolding)
        {
            Vector2 spawnPosition = GetRandomPosition();
            currentTarget = Instantiate(targetPrefab, spawnPosition, Quaternion.identity);

            // ===== Инициализируем прогресс-бар =====
            var lifetimeUI = currentTarget.GetComponent<TargetLifetimeUI>();
            if (lifetimeUI != null)
                lifetimeUI.Init(targetLifetime);
            // ========================================

            button.isTooEarly = false;
            button.isTargetActive = true;

            // Уничтожить через время (и полоска сама остановится)
            Destroy(currentTarget, targetLifetime);
        }

        canSpawn = true;
    }
    private Vector2 GetRandomPosition()
    {
        if (spawnArea == null)
        {
            return new Vector2(
                Random.Range(-5f, 5f),
                Random.Range(-3f, 3f)
            );
        }
        else
        {
            Bounds bounds = spawnArea.bounds;
            return new Vector2(
                Random.Range(bounds.min.x, bounds.max.x),
                Random.Range(bounds.min.y, bounds.max.y)
            );
        }
    }

    public bool IsCursorOverTarget()
    {
        if (currentTarget == null) return false;

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D hit = Physics2D.OverlapPoint(mousePosition, LayerMask.GetMask("Target"));
        if (audioFireSource != null)
            audioFireSource.Play();
        switch (hit?.gameObject.tag)
        {
            case "Inner":
                {
                    Hit(3);
                    CheckLife();
                    controller.enemyStep = true;
                    enemyTurnEffect.PlayEnemyTurnEffect();
                    return true;
                }
            case "Middle":
                {
                    Hit(2);
                    CheckLife();
                    controller.enemyStep = true;
                    enemyTurnEffect.PlayEnemyTurnEffect();
                    return true;
                }
            case "Outer":
                {
                    Hit(1);
                    CheckLife();
                    controller.enemyStep = true;
                    enemyTurnEffect.PlayEnemyTurnEffect();
                    return true;
                }
            default:
                {
                    CheckLife();
                    controller.enemyStep = true;
                    enemyTurnEffect.PlayEnemyTurnEffect();
                    return false;
                }
        };
    }

    public void HideTarget()
    {
        if (currentTarget != null)
        {
            Destroy(currentTarget);
            currentTarget = null;
        }
    }

    private void CheckLife()
    {
        if (hpEnemy.transform.childCount == 0)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    private void Hit(int damage)
    {
        int hpCount = hpEnemy.transform.childCount;
        int damageToApply = Mathf.Min(damage, hpCount); // Не больше, чем есть HP
        int lastIndex = hpEnemy.transform.childCount - 1; // Обновляем каждый раз, так как дочерние объекты исчезают


        for (int i = 0; i < damageToApply; i++)
        {
            Destroy(hpEnemy.transform.GetChild(lastIndex--).gameObject);
        }
    }
}
