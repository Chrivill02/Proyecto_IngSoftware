using UnityEngine;

public class GlueBullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 1f;

    private Rigidbody2D rb;
    private float direction = 1f;

    public void SetDirection(float dir)
    {
        direction = dir;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;


        // Mueve la bala en la direcci�n correcta
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
        if (collision.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject); // mata al enemigo
            Destroy(gameObject);           // destruye la bala
        }

        if (collision.CompareTag("Breakable"))
        {
            Destroy(collision.gameObject); // destruye pared
            Destroy(gameObject);
        }
    }
}