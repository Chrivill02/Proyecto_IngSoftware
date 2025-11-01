using System;
using UnityEngine;




public class Player : MonoBehaviour, PlayerMovementInputObserver
{
    public float jumpForce;
    public float speed = 5f;
    private Rigidbody2D Rigidbody2D;
    private float movementInput;
    private Animator animator;
    public Transform groundDetector;
    public float radioDetector = 0.1f;
    public LayerMask groundLayer;
    private bool isGrounded;
    private bool isDead = false;
    public event Action OnPlayerDeath;

    void Start()
    {
        animator = GetComponent<Animator>();
        Rigidbody2D = GetComponent<Rigidbody2D>();

        PlayerInputManager inputManager = FindFirstObjectByType<PlayerInputManager>();
        inputManager.OnJumpKeyPressed += OnJumpKeyPressed;
        inputManager.OnMoveKeyPressed += OnMove;
    }

    void Update()
    {
        if (isDead) return;
        isGrounded = detectGrounded();
    }

    private void FixedUpdate()
    {
        if (isDead) return;

        Rigidbody2D.linearVelocity = new Vector2(movementInput * speed, Rigidbody2D.linearVelocity.y);

        animator.SetBool("estaCorriendo", movementInput != 0);

        // Cambiar dirección
        if (movementInput < 0)
            transform.localScale = new Vector3(-1, 1, 1);
        else if (movementInput > 0)
            transform.localScale = new Vector3(1, 1, 1);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isGrounded)
            animator.SetBool("estaSaltando", false);

        if (collision.gameObject.CompareTag("Obstaculo") || collision.gameObject.CompareTag("Enemy"))
        {
            OnPlayerDeath?.Invoke();
            isDead = true;
        }
    }

    public void OnJumpKeyPressed()
    {
        if (isGrounded)
        {
            animator.SetBool("estaSaltando", true);
            Rigidbody2D.AddForce(new Vector2(0, jumpForce));
        }
    }

    public void OnMove(float direction)
    {
        movementInput = direction;
    }

    public bool detectGrounded()
    {
        Collider2D hitbox = Physics2D.OverlapCircle(groundDetector.position, radioDetector, groundLayer);
        return hitbox != null;
    }
    
}
