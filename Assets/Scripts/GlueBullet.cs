using UnityEngine;

public class GlueBullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 1f;
    public float impactAnimationDuration = 0.3f;

    private Rigidbody2D rb;
    private Animator anim; // El controlador de animación
    private float direction = 1f;
    private bool hasHit = false; // Para controlar la colisión

    public void SetDirection(float dir)
    {
        direction = dir;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>(); // Obtenemos el componente Animator
        rb.gravityScale = 0;

        rb.linearVelocity = new Vector2(speed * direction, 0);

        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            Collider2D playerCollider = player.GetComponent<Collider2D>();
            Collider2D bulletCollider = GetComponent<Collider2D>();
            if (playerCollider != null && bulletCollider != null)
            {
                Physics2D.IgnoreCollision(bulletCollider, playerCollider);
            }
        }

        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasHit)
        {
            return;
        }

        if (collision.CompareTag("Enemy") || collision.CompareTag("FinalChief") || collision.CompareTag("Breakable") || collision.CompareTag("Minion"))
        {
            hasHit = true;
            rb.linearVelocity = Vector2.zero;
            anim.SetTrigger("Impact");

            
            if (collision.GetComponent<JefeFinal>() != null)
            {
                JefeFinal jefe = collision.GetComponent<JefeFinal>();
                // Llama a su función de daño (pasamos 'null' porque la bala no rebota)
                jefe.RecibirDano(null);
            }
            // 3. ¿Es otra cosa (ej. "Breakable")?
            else
            {
                Destroy(collision.gameObject);
            }

            // --- Fin de la Lógica ---

            // Destruye la bala después de que la animación termine
            Destroy(gameObject, impactAnimationDuration);
        }
    }
}