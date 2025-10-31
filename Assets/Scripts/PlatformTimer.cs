using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlatformTimer : MonoBehaviour
{
    [Header("Movement Settings")]
    public float constantUpwardSpeed = 0.5f; // <-- NUEVA VARIABLE: Controla la velocidad constante
    public float verticalLimit = 10.0f;      // Límite superior que la plataforma no puede rebasar

    [Header("Spawning")]
    public FlyingEnemySpawner flyingEnemySpawner;

    private Rigidbody2D rb;
    private bool isPlayerOnPlatform = false; // <-- NUEVO: Un interruptor para saber si el jugador está encima

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
    }

    // FixedUpdate es el mejor lugar para la física. Se ejecuta en un intervalo de tiempo fijo.
    void FixedUpdate()
    {
        // Solo se mueve si el jugador está en la plataforma Y no hemos alcanzado el límite de altura.
        if (isPlayerOnPlatform && transform.position.y < verticalLimit)
        {
            // Calcula la nueva posición hacia arriba basándose en la velocidad y el tiempo.
            Vector2 newPosition = rb.position + Vector2.up * constantUpwardSpeed * Time.fixedDeltaTime;
            rb.MovePosition(newPosition); // Mueve el Rigidbody a la nueva posición.
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
            isPlayerOnPlatform = true; // <-- ACTIVAMOS el interruptor de movimiento

            if (flyingEnemySpawner != null)
            {
                flyingEnemySpawner.StartSpawning();
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
            isPlayerOnPlatform = false; // <-- DESACTIVAMOS el interruptor, la plataforma se detendrá

            if (flyingEnemySpawner != null)
            {
                flyingEnemySpawner.StopSpawning();
            }
        }
    }
}