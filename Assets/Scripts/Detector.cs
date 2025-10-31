using UnityEngine;

public class DetectorPlataforma : MonoBehaviour
{
    private PlataformaSemisolida plataforma;

    void Start()
    {
        plataforma = GetComponentInParent<PlataformaSemisolida>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody2D playerRb = other.attachedRigidbody;
            // Si el jugador viene cayendo
            if (playerRb != null && playerRb.linearVelocity.y <= 0)
            {
                plataforma.ActivarPlataforma(true);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            plataforma.ActivarPlataforma(false);
        }
    }
}
