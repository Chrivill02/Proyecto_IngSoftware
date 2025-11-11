using UnityEngine;
using System.Collections;

public class CanvasFade : MonoBehaviour
{
 public CanvasGroup canvasGroup;
    public float fadeDuration = 1.0f;

    void Start()
    {
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        canvasGroup.alpha = 0;
        canvasGroup.gameObject.SetActive(true);

        float elapsed = 0;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(elapsed / fadeDuration);
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        FadeOutAndDisable();
    }

    public void FadeOutAndDisable()
    {
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        float elapsed = 0;
        float startAlpha = canvasGroup.alpha;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, 0, elapsed / fadeDuration);
            yield return null;
        }
        yield return new WaitForSeconds(2f);
        canvasGroup.gameObject.SetActive(false);
    }
}
