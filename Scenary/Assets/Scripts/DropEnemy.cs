// DropEnemy.cs
using UnityEngine;

// Renombramos la clase y añadimos Damageable
public class DropEnemy : MonoBehaviour, FallingEnemy, Damageable
{
    public int health = 1; // Vida de la gota

    private Vector3 initialPosition;
    private Quaternion rotacionInicial;

    void Start()
    {
        initialPosition = transform.position;
        rotacionInicial = transform.rotation;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Suelo") ||
            collision.gameObject.tag == "Obstaculo" ||
            collision.gameObject.tag == "Player" || // Deberías quitar "Player" si ya es Damageable
            collision.gameObject.tag == "Enemy")
        {
            OnGroundDetected();
        }

        // También comprobamos si chocamos con algo dañable (como el jugador)
        Damageable damageableObject = collision.gameObject.GetComponent<Damageable>();
        if (damageableObject != null)
        {
            damageableObject.RecibirDano(1); // La gota hace daño al chocar
            OnGroundDetected(); // Se resetea al chocar
        }
    }

    public void OnGroundDetected()
    {
        // Esta es la lógica de "muerte" o reseteo de la gota
        transform.position = initialPosition;
        transform.rotation = rotacionInicial;
    }

    // --- Implementación de Damageable ---
    public void RecibirDano(int cantidad)
    {
        health -= cantidad;
        if (health <= 0)
        {
            // Al recibir daño (ej. un disparo), se resetea.
            Destroy(gameObject);
        }
    }
}