using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TargetManager : MonoBehaviour
{
    public GameObject targetPrefab;
    public BoxCollider2D spawnArea;
    private GameObject currentTarget;
    public HoldButton? button;
    public TutorialHoldButton? tutorialButton;
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



        if (button != null && button.isHolding)
        {
            Vector2 spawnPosition = GetRandomPosition();
            currentTarget = Instantiate(targetPrefab, spawnPosition, Quaternion.identity);
            float perk = PlayerPrefs.GetInt("Perk_Accuracy");

            //Debug.Log($"мишень {currentTarget.GetComponent<SmoothShrink>()!=null}");
            var shrink = currentTarget.GetComponent<SmoothShrink>();
            // вычисляем новое значение
            float newSpeed = Mathf.Max(shrink.shrinkSpeed - 0.2f * perk, 0f);
            // присваиваем
            shrink.shrinkSpeed = newSpeed;

            //Debug.Log($"[Before] target name={currentTarget.name}, shrinkSpeed={shrink.shrinkSpeed}, perk={perk}");

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

        if (tutorialButton != null && tutorialButton.isHolding)
        {
            Debug.Log("Спавнится");
            Vector2 spawnPosition = GetRandomPosition();
            currentTarget = Instantiate(targetPrefab, spawnPosition, Quaternion.identity);

            // ===== Инициализируем прогресс-бар =====
            var lifetimeUI = currentTarget.GetComponent<TargetLifetimeUI>();
            if (lifetimeUI != null)
                lifetimeUI.Init(targetLifetime);
            // ========================================

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

        var damage = PlayerPrefs.GetInt("PlayerDamage");

        Debug.Log("Кобура" + $"{button.GetComponent<Button>().interactable != null}");
        if (audioFireSource != null)
            audioFireSource.Play();
        switch (hit?.gameObject.tag)
        {
            case "Inner":
                {
                    controller.DamageEnemy(damage + damage * 30 / 100);
                    controller.UpdateHealthDisplays();
                    controller.enemyStep = true;
                    button.GetComponent<Button>().interactable = false;
                    enemyTurnEffect.PlayEnemyTurnEffect();
                    return true;
                }
            case "Middle":
                {
                    controller.DamageEnemy(damage + damage * 10 / 100);
                    controller.UpdateHealthDisplays();
                    controller.enemyStep = true;
                    button.GetComponent<Button>().interactable = false;
                    enemyTurnEffect.PlayEnemyTurnEffect();
                    return true;
                }
            case "Outer":
                {
                    controller.DamageEnemy(damage);
                    controller.UpdateHealthDisplays();
                    controller.enemyStep = true;
                    button.GetComponent<Button>().interactable = false;
                    enemyTurnEffect.PlayEnemyTurnEffect();
                    return true;
                }
            default:
                {
                    button.GetComponent<Button>().interactable = false;
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
}