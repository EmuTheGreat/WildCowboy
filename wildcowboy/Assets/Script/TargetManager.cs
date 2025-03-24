using System.Collections;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    public GameObject targetPrefab; // Префаб мишени
    public BoxCollider2D spawnArea; // Коллайдер, внутри которого будет появляться мишень
    private GameObject currentTarget;
    public HoldButton button;
    public bool canSpawn = false;
    public float targetLifetime = 5f;


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

        // Ждем случайное время
        float delay = Random.Range(minSpawnDelay, maxSpawnDelay);
        yield return new WaitForSeconds(delay);

        if (button.isHolding)
        {
            Vector2 spawnPosition = GetRandomPosition();
            currentTarget = Instantiate(targetPrefab, spawnPosition, Quaternion.identity);
            button.isTooEarly = false;
            button.isTargetActive = true;
        }

        Destroy(currentTarget, targetLifetime);

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

        return (hit?.gameObject.tag) switch
        {
            "Inner" => true,
            "Middle" => true,
            "Outer" => true,
            _ => false,
        };
    }

    public void HideTarget()
    {
        if (currentTarget != null)
        {
            Destroy(currentTarget);
            currentTarget = null;
            Debug.Log("Мишень скрыта.");
        }
    }
}
