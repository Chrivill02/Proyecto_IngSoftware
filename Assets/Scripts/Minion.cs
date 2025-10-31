using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MinionSigueJugador : MonoBehaviour
{
    [Header("Estadísticas")]
    public float speed = 2.5f;
    public int vida = 1; // Vida por si el jugador le salta encima

    [Header("Combate")]
    public float fuerzaReboteJugador = 8f; // Rebote para el jugador

    // --- Variables Privadas ---
    private Transform jugadorTransform;
    private Rigidbody2D rb;
    private bool estaVivo = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // 1. Encontrar al jugador automáticamente por su Tag
        GameObject jugador = GameObject.FindGameObjectWithTag("Player");
        if (jugador != null)
        {
            jugadorTransform = jugador.transform;
        }
        else
        {
            // Si no encuentra al jugador, se autodestruye
            Debug.LogWarning("Minion no pudo encontrar al 'Player'. Autodestruyendo.");
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        // Si no está vivo o no hay jugador, no hacer nada
        if (!estaVivo || jugadorTransform == null)
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            return;
        }

        // --- Lógica de Persecución ---

        // 1. Calcular la dirección horizontal hacia el jugador
        float direccionHorizontal = Mathf.Sign(jugadorTransform.position.x - transform.position.x);

        // 2. Moverse en esa dirección
        rb.linearVelocity = new Vector2(direccionHorizontal * speed, rb.linearVelocity.y);

        // 3. Girar el sprite
        Girar(direccionHorizontal);
    }

    private void Girar(float direccion)
    {
        // Gira el sprite para que mire en la dirección del movimiento
        if (direccion < 0)
        {
            // Mirar a la izquierda
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (direccion > 0)
        {
            // Mirar a la derecha
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    // --- Colisiones (solo si el jugador salta encima) ---

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!estaVivo) return;

        // Comprobar si el jugador saltó encima del minion
        if (collision.gameObject.CompareTag("Player"))
        {
            ContactPoint2D puntoContacto = collision.GetContact(0);

            // Si el jugador le salta encima
            if (puntoContacto.normal.y < -0.5f)
            {
                RecibirDano(collision.gameObject);
            }
            // Si el minion choca al jugador por el lado,
            // tu script "Jugador.cs" se encargará del Game Over
            // (porque este minion tendrá el Tag "Enemy")
        }
    }

    private void RecibirDano(GameObject jugador)
    {
        vida--;

        // Hacer rebotar al jugador
        Rigidbody2D rbJugador = jugador.GetComponent<Rigidbody2D>();
        if (rbJugador != null)
        {
            rbJugador.linearVelocity = new Vector2(rbJugador.linearVelocity.x, 0);
            rbJugador.AddForce(Vector2.up * fuerzaReboteJugador, ForceMode2D.Impulse);
        }

        if (vida <= 0)
        {
            Morir();
        }
    }

    // Esta función es solo para cuando el jugador lo mata saltando
    private void Morir()
    {
        estaVivo = false;
        GetComponent<Collider2D>().enabled = false;
        rb.linearVelocity = Vector2.zero;
        Destroy(gameObject);
    }
}