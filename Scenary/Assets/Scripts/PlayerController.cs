using System;
using UnityEngine;

public class Jugador : MonoBehaviour
{
    private bool tieneLlave = false; // Para saber si el jugador recogió la llave

    public float fuerzaSalto;
    public GameManager gameManager;  // 🔹 Corregido

    public float speed = 5f;
    private Rigidbody2D rb;          // 🔹 Corregido
    private float inputMovimiento;
    private Animator animator;

    public Transform detectorSuelo;
    public float radioDetector = 0.1f;
    public LayerMask layerSuelo;
    private bool estaEnSuelo;

    private bool estaMuerto = false;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (estaMuerto) return;

        Collider2D golpe = Physics2D.OverlapCircle(detectorSuelo.position, radioDetector, layerSuelo);
        estaEnSuelo = golpe && Mathf.Abs(golpe.transform.up.y) > 0.9f;

        if (estaEnSuelo && Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("estaSaltando", true);
            rb.AddForce(new Vector2(0, fuerzaSalto));
        }

        inputMovimiento = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate()
    {
        if (estaMuerto) return;

        rb.linearVelocity = new Vector2(inputMovimiento * speed, rb.linearVelocity.y);
        animator.SetBool("estaCorriendo", inputMovimiento != 0);

        if (inputMovimiento < 0)
            transform.localScale = new Vector3(-1, 1, 1);
        else if (inputMovimiento > 0)
            transform.localScale = new Vector3(1, 1, 1);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (estaEnSuelo)
            animator.SetBool("estaSaltando", false);

        if (collision.gameObject.CompareTag("Obstaculo") || collision.gameObject.CompareTag("Enemy"))
        {
            gameManager.gameOver = true;

            if (!estaMuerto)
                StartCoroutine(EfectoDeMuerte());
        }
    }

    private System.Collections.IEnumerator EfectoDeMuerte()
    {
        estaMuerto = true;
        animator.enabled = false;
        rb.linearVelocity = Vector2.zero;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        float duracion = 1.0f;
        float tiempo = 0f;
        Color colorInicial = spriteRenderer.color;
        Vector3 escalaInicial = transform.localScale;

        while (tiempo < duracion)
        {
            tiempo += Time.deltaTime;
            float t = tiempo / duracion;

            spriteRenderer.color = new Color(colorInicial.r, colorInicial.g, colorInicial.b, Mathf.Lerp(1f, 0.2f, t));
            transform.localScale = Vector3.Lerp(escalaInicial, escalaInicial * 0.2f, t);

            yield return null;
        }

        spriteRenderer.color = new Color(colorInicial.r, colorInicial.g, colorInicial.b, 0.2f);
        transform.localScale = escalaInicial * 0.2f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Key"))
        {
            tieneLlave = true;
            Destroy(collision.gameObject);
            Debug.Log("Has recogido la llave 🗝️");
        }

        if (collision.CompareTag("Door"))
        {
            if (tieneLlave)
            {
                Debug.Log("Puerta abierta 🚪");
                Destroy(collision.gameObject);
            }
            else
            {
                Debug.Log("La puerta está cerrada. Necesitas la llave 🔒");
            }
        }
    }
}
