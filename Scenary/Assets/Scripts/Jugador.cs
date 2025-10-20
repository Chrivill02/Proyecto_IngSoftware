using System;
using UnityEngine;

public class Jugador : MonoBehaviour
{
    public float fuerzaSalto;
    public NewMonoBehaviourScript gameManager;

    public float speed = 5f;
    private Rigidbody2D rigidbody2D;
    private float inputMovimiento;
    private Animator animator;

    public Transform detectorSuelo; 
    public float radioDetector = 0.1f;
    public LayerMask layerSuelo;
    private bool estaEnSuelo;

    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Si el juego terminó, no hacer nada
        if (gameManager != null && gameManager.gameOver)
            return;

        // Detectar si está en el suelo
        Collider2D golpe = Physics2D.OverlapCircle(detectorSuelo.position, radioDetector, layerSuelo);
        estaEnSuelo = golpe && Mathf.Abs(golpe.transform.up.y) > 0.9f;

        // Saltar
        if (estaEnSuelo && Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("estaSaltando", true);
            rigidbody2D.AddForce(new Vector2(0, fuerzaSalto));
        }

        inputMovimiento = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate()
    {
        // Si el juego terminó, detener movimiento
        if (gameManager != null && gameManager.gameOver)
        {
            rigidbody2D.linearVelocity = Vector2.zero;
            return;
        }

        rigidbody2D.linearVelocity = new Vector2(inputMovimiento * speed, rigidbody2D.linearVelocity.y);

        animator.SetBool("estaCorriendo", inputMovimiento != 0);

        if (inputMovimiento < 0)
            transform.localScale = new Vector3(-1, 1, 1);
        else if (inputMovimiento > 0)
            transform.localScale = new Vector3(1, 1, 1);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (estaEnSuelo)
        {
            animator.SetBool("estaSaltando", false);
        }

        // Si colisiona con un obstáculo o enemigo
        if (collision.gameObject.CompareTag("Obstaculo") || collision.gameObject.CompareTag("Enemy"))
        {
            gameManager.gameOver = true;
            animator.SetBool("estaCorriendo", false);
            animator.SetBool("estaSaltando", false);
        }
    }
}
