using UnityEngine;
public abstract class Proyectil : MonoBehaviour
{
    [SerializeField] protected float speed = 10f;
    [SerializeField] protected int dano = 1;
    [SerializeField] protected float lifetime = 2f;

    protected Rigidbody2D rb;
    protected float direction = 1f;
    protected bool hasHit = false;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
    }
    protected virtual void Start()
    {
        Destroy(gameObject, lifetime);
        IgnorePlayerCollision();
    }
    // M�todo para configurar desde la Factory
    public virtual void Initialize(float dir)
    {
        direction = dir;
        rb.linearVelocity = new Vector2(speed * direction, 0);
        // Ajustar la escala si el sprite necesita voltearse
        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * direction, transform.localScale.y, transform.localScale.z);
    }
    // L�gica b�sica de colisi�n (puede ser sobreescrita)
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasHit) return;
        // Implementaci�n espec�fica en clases hijas
    }
    private void IgnorePlayerCollision()
    {
        GameObject player = GameObject.FindWithTag("Player"); // Asume que tu jugador tiene el tag "Player"
        if (player != null)
        {
            Collider2D playerCollider = player.GetComponent<Collider2D>();
            Collider2D bulletCollider = GetComponent<Collider2D>(); // Obtiene el collider de esta bala

            if (playerCollider != null && bulletCollider != null)
            {
                Physics2D.IgnoreCollision(bulletCollider, playerCollider);
                Debug.Log("Ignorando colisión entre bala y jugador.");
            }
            else
            {
                Debug.LogWarning("No se encontró Collider2D en la bala o el jugador para ignorar colisión.", this);
            }
        }
        else
        {
            Debug.LogWarning("No se encontró GameObject con tag 'Player' para ignorar colisión.", this);
        }
    }
}