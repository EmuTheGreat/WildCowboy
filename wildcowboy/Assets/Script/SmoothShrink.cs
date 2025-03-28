using UnityEngine;

public class SmoothShrink : MonoBehaviour
{
    public Vector3 targetScale = Vector3.zero; // до какого размера уменьшать
    public float shrinkSpeed = 1f; // скорость уменьшения

    private void Update()
    {
        // Плавное уменьшение масштаба объекта
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * shrinkSpeed);

        // Можно остановить, когда почти достигли цели
        if (Vector3.Distance(transform.localScale, targetScale) < 0.01f)
        {
            transform.localScale = targetScale;
            enabled = false; // Отключаем скрипт, чтобы не продолжал работать
        }
    }
}
