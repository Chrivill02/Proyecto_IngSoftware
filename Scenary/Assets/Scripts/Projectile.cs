// Projectile.cs
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 1f;
    public GameObject explosionPrefab;
    public int damageAmount = 1; // Añadimos cuánto daño hace

    private Rigidbody2D rb;
    private float direction = 1f;

    public virtual void SetDirection(float dir)
    {
        direction = dir;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.linearVelocity = new Vector2(speed * direction, 0);

        // Ignorar colisión con el jugador
        Collider2D playerCollider = GameObject.FindWithTag("Player").GetComponent<Collider2D>();
        Collider2D bulletCollider = GetComponent<Collider2D>();
        Physics2D.IgnoreCollision(bulletCollider, playerCollider);

        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.isTrigger) return;

        // --- INICIO DE LA REFACTORIZACIÓN ---
        // Buscamos cualquier componente que implemente Damageable
        Damageable damageableObject = collision.GetComponent<Damageable>();

        if (damageableObject != null)
        {
            // Si lo encontramos, le infligimos daño
            damageableObject.RecibirDano(damageAmount);
        }

        // La bala se destruye al chocar con CUALQUIER cosa sólida 
        // (excepto el jugador, que ya lo ignoramos)
        ExplodeAndDestroy();
        // --- FIN DE LA REFACTORIZACIÓN ---
    }

    private void ExplodeAndDestroy()
    {
        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}