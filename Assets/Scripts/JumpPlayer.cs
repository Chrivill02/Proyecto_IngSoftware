using UnityEngine;


public class PlayerJump : MonoBehaviour
{
    public float jumpForce = 1f; // fuerza del salto
    private Rigidbody2D rb;
    private bool isGrounded = true; // para evitar saltos infinitos

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // agarramos el rigidbody del jugador
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce); // salto
            isGrounded = false; // ya no est� en el suelo
        }
    }

    // Detectar cuando toca el suelo
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Suelo"))
        {
            isGrounded = true;
        }
    }
}