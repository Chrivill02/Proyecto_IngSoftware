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

    private bool estaMuerto = false;        
    private SpriteRenderer spriteRenderer;  

    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
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
            rigidbody2D.AddForce(new Vector2(0, fuerzaSalto));
        }

        inputMovimiento = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate()
    {
        if (estaMuerto) return; 

        rigidbody2D.linearVelocity = new Vector2(inputMovimiento * speed, rigidbody2D.linearVelocity.y);

        animator.SetBool("estaCorriendo", inputMovimiento != 0);

        // Cambiar dirección
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
        rigidbody2D.linearVelocity = Vector2.zero;
        rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;

        float duracion = 1.0f;
        float tiempo = 0f;
        Color colorInicial = spriteRenderer.color;
        Vector3 escalaInicial = transform.localScale;

        while (tiempo < duracion)
        {
            tiempo += Time.deltaTime;
            float t = tiempo / duracion;

            // Bajar opacidad y hacer más pequeño
            spriteRenderer.color = new Color(colorInicial.r, colorInicial.g, colorInicial.b, Mathf.Lerp(1f, 0.2f, t));
            transform.localScale = Vector3.Lerp(escalaInicial, escalaInicial * 0.2f, t);

            yield return null;
        }

        spriteRenderer.color = new Color(colorInicial.r, colorInicial.g, colorInicial.b, 0.2f);
        transform.localScale = escalaInicial * 0.2f;
    }
}
