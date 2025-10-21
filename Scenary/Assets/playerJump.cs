using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public float jumpForce = 7f;       // Qu� tan alto salta
    public Transform groundCheck;      // Punto para detectar el suelo
    public float checkRadius = 0.2f;   // Radio de detecci�n
    public LayerMask groundLayer;      // Qu� capas son "suelo"

    private Rigidbody2D rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Detectar si est� tocando el suelo
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);

        // Saltar al presionar espacio
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    // Mostrar c�rculo de detecci�n en el editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
    }
}
