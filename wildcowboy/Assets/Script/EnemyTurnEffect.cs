using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;


public class EnemyTurnEffect : MonoBehaviour
{
    public Image blackOverlay;        // Сюда перетаскиваешь затемняющий Image
    public float fadeDuration = 1f;   // Время затемнения
    public AudioSource audioSource;   // Сюда перетаскиваешь AudioSource со звуком
    public AudioSource audioFireSource;
    public GameController gameController;

    private void Start()
    {
        if (blackOverlay != null)
        {
            var color = blackOverlay.color;
            blackOverlay.color = new Color(color.r, color.g, color.b, 0f);
        }

        if (audioSource != null)
        {
            audioSource.playOnAwake = false;
        }
    }

    public void PlayEnemyTurnEffect()
    {
        StartCoroutine(PlayEffectRoutine());
        gameController.enemyStep = false;
    }

    private IEnumerator PlayEffectRoutine()
    {
        // Включаем затемнение
        yield return StartCoroutine(FadeImage(0f, 0.7f));

        // Воспроизводим звук
        if (audioSource != null)
            audioSource.Play();

        // Немного подождем, пока идет ход
        yield return new WaitForSeconds(1.5f);

        // Убираем затемнение
        yield return StartCoroutine(FadeImage(0.7f, 0f));
        if (audioFireSource != null)
            audioFireSource.Play();

        gameController.DamageSelf(Random.Range(gameController.EnemyDamage, gameController.EnemyDamage + gameController.EnemyDamage*30/100));

    }

    private IEnumerator FadeImage(float from, float to)
    {
        float elapsed = 0f;
        Color color = blackOverlay.color;

        while (elapsed < fadeDuration)
        {
            float alpha = Mathf.Lerp(from, to, elapsed / fadeDuration);
            blackOverlay.color = new Color(color.r, color.g, color.b, alpha);
            elapsed += Time.deltaTime;
            yield return null;
        }

        blackOverlay.color = new Color(color.r, color.g, color.b, to);
    }
}
