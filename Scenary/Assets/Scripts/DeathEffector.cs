using UnityEngine;




public class DeathHandler1 : MonoBehaviour, PlayerObserver
{
    public GameObject player;
    private Animator animator;
    private Rigidbody2D Rigidbody2D;
    private SpriteRenderer spriteRenderer;
    private Transform playerTransform;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = player.GetComponent<Animator>();
        Rigidbody2D = player.GetComponent<Rigidbody2D>();
        spriteRenderer = player.GetComponent<SpriteRenderer>();
        playerTransform = player.GetComponent<Transform>();

        Player playerScript = player.GetComponent<Player>();
        playerScript.OnPlayerDeath += OnPlayerDeath;
    }

    public void OnPlayerDeath()
    {
        StartCoroutine(DeathEffect());
    }

    private System.Collections.IEnumerator DeathEffect()
    {

        animator.enabled = false;
        Rigidbody2D.linearVelocity = Vector2.zero;
        Rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;

        float duracion = 1.0f;
        float tiempo = 0f;
        Color colorInicial = spriteRenderer.color;
        Vector3 escalaInicial = playerTransform.localScale;

        while (tiempo < duracion)
        {
            tiempo += Time.deltaTime;
            float t = tiempo / duracion;

            // Bajar opacidad y hacer más pequeño
            spriteRenderer.color = new Color(colorInicial.r, colorInicial.g, colorInicial.b, Mathf.Lerp(1f, 0.2f, t));
            playerTransform.localScale = Vector3.Lerp(escalaInicial, escalaInicial * 0.2f, t);

            yield return null;
        }

        spriteRenderer.color = new Color(colorInicial.r, colorInicial.g, colorInicial.b, 0.2f);
        playerTransform.localScale = escalaInicial * 0.2f;
    }
}
