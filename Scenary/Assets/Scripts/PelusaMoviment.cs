using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float JumpForce;
    public float Speed;
    public float FuerzaRebote;

    public NewMonoBehaviourScript gameManager; // Conecta tu GameManager aquí

    private Rigidbody2D Rigidbody2D;
    private Animator Animator;
    private bool recibirDaño;
    private float Horizontal;
    private bool Grouded;

    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (gameManager != null && !gameManager.start)
            return; // Si el juego no empezó, no hace nada
        
        Horizontal = Input.GetAxisRaw("Horizontal");

        if (Horizontal < 0.0f) transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else if (Horizontal > 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        Animator.SetBool("running", Horizontal != 0.0f);

        Debug.DrawRay(transform.position, Vector3.down * 1.5f, Color.red);
        if (Physics2D.Raycast(transform.position, Vector3.down, 1.5f))
        {
            Grouded = true;
        }
        else Grouded = false;

        if (Input.GetKeyDown(KeyCode.W) && Grouded && !recibirDaño)
        {
            Jump();
        }
    }

    private void Jump()
    {
        Rigidbody2D.AddForce(Vector2.up * JumpForce);
    }

    private void FixedUpdate()
    {
        Rigidbody2D.linearVelocity = new Vector2(Horizontal * Speed, Rigidbody2D.linearVelocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Si colisiona con obstáculo o enemigo, activa Game Over
        if (collision.gameObject.CompareTag("Obstaculo") || collision.gameObject.CompareTag("Enemy"))
        {
            if (gameManager != null)
            {
                gameManager.gameOver = true;
            }
        }
    }

    public void RecibirDanio(Vector2 direccion, int canDanio)
    {
        recibirDaño = true;
        Vector2 rebote = new Vector2(transform.position.x - direccion.x, 1).normalized;
        Rigidbody2D.AddForce(rebote * FuerzaRebote, ForceMode2D.Impulse);
    }

    public void DesactivaDani()
    {
        recibirDaño = false;
    }
}
