using UnityEngine;

public class ExplosionLifespan : MonoBehaviour
{
    public float lifetime = 1f;

    public float fadeDelay = 1f;
    public float fadeDuration = 1f;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;

        Invoke(nameof(startFade), fadeDelay);
        Destroy(gameObject, lifetime);
    }

    void startFade()
    {
        StartCoroutine(FadeOut());
    }

    System.Collections.IEnumerator FadeOut()
    {
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / fadeDuration;

            float newAlpha = Mathf.Lerp(originalColor.a, 0f, t);
            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, newAlpha);

            yield return null;
        }

        spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);      
    }
}
