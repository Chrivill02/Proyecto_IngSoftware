using UnityEngine;




public class Projectile : MonoBehaviour
{
  public float speed = 10f;
  public float lifetime = 1f;
  public GameObject explosionPrefab;

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

    // Mueve la bala en la dirección correcta
    rb.linearVelocity = new Vector2(speed * direction, 0);

    // Ignorar colision con el jugador
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
      Destroy(collision.gameObject);
      ExplodeAndDestroy();
    }
    else if (collision.CompareTag("Breakable"))
    {
      Destroy(collision.gameObject);
      ExplodeAndDestroy();
    }
    else
    {
      // Si choca con cualquier otra cosa solida, también explota
      ExplodeAndDestroy();
    }
  }

  private void ExplodeAndDestroy()
  {
    // Crear el efecto de explosión/mancha
    if (explosionPrefab != null)
    {
      Instantiate(explosionPrefab, transform.position, Quaternion.identity);
    }

    // Destruir la bala
    Destroy(gameObject);
  }
}
