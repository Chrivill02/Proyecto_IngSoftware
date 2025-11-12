// ChaserEnemy.cs
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
// Implementamos Damageable además de Enemy
public class ChaserEnemy : MonoBehaviour, Enemy, Damageable
{
    [Header("Chaser Stats")]
    public float speed = 3f;
    public int health = 3; // Añadimos vida

    [Header("Chasing Logic")]
    public string playerTag = "Player";
    protected Rigidbody2D rb;
    protected Transform player;
    protected bool chasing = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
    }

    void FixedUpdate()
    {
        if (chasing && player != null)
        {
            Vector2 targetVelocity = CalculateTargetVelocity();
            rb.linearVelocity = targetVelocity;
            FlipSprite(targetVelocity.x);
        }
        else
        {
            rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(playerTag)) return;
        player = other.transform;
        chasing = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            chasing = false;
            player = null;
        }
    }

    void FlipSprite(float dirX)
    {
        if (dirX == 0) return;
        Vector3 s = transform.localScale;
        s.x = Mathf.Abs(s.x) * (dirX < 0 ? -1 : 1);
        transform.localScale = s;
    }

    Vector2 CalculateTargetVelocity()
    {
        float dirX = Mathf.Sign(player.position.x - transform.position.x);
        float distX = Mathf.Abs(player.position.x - transform.position.x);
        float moveX = distX > 0.05f ? dirX * speed : 0f;

        return new Vector2(moveX, rb.linearVelocity.y);
    }

    // --- Implementación de Damageable ---
    public void RecibirDano(int cantidad)
    {
        health -= cantidad;
        if (health <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        // Lógica de muerte (ej. animación, partículas, dropear item)
        Destroy(gameObject);
    }
}