using System;
using UnityEngine;


public class Jugador : MonoBehaviour, Damageable
{ // Implementa Damageable
    [Header("Movement")]
    [SerializeField] private float fuerzaSalto = 1f;
    [SerializeField] private float speed = 5f;
    [SerializeField] private Transform detectorSuelo;
    [SerializeField] private float radioDetector = 0.1f;
    [SerializeField] private LayerMask layerSuelo;

    [Header("Stats")]
    [SerializeField] private int vidaInicial = 1; // Asumimos 1 vida por simplicidad
    private int vidaActual;

    
    public event Action OnMuerteJugador; 

    private Rigidbody2D rb;
    private Animator animator;
    private float inputMovimiento;
    private bool estaEnSuelo;
    // private bool isDead = false; // Para evitar acciones post-muerte

    void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        vidaActual = vidaInicial;
    }

    void Update()
    {
        if (vidaActual <= 0) return; 

        
        Collider2D golpe = Physics2D.OverlapCircle(detectorSuelo.position, radioDetector, layerSuelo);
        bool estabaEnSuelo = estaEnSuelo;
        estaEnSuelo = golpe != null; 

       
        if (estaEnSuelo && !estabaEnSuelo && rb.linearVelocity.y <= 0.1f)
        {
            animator.SetBool("estaSaltando", false);
        }

        
        if (estaEnSuelo && Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("estaSaltando", true);
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0); 
            rb.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse); 
        }
        inputMovimiento = Input.GetAxis("Horizontal");

        
        animator.SetBool("estaCorriendo", Mathf.Abs(inputMovimiento) > 0.1f && estaEnSuelo);

        
        GirarSprite();
    }

    void FixedUpdate()
    {
        if (vidaActual <= 0)
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y); 
            return;
        }
        
        rb.linearVelocity = new Vector2(inputMovimiento * speed, rb.linearVelocity.y);
    }

    private void GirarSprite()
    {
        if (Mathf.Abs(inputMovimiento) > 0.01f)
        { 
            transform.localScale = new Vector3(Mathf.Sign(inputMovimiento), 1, 1);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (vidaActual <= 0) return;
       
        if (collision.gameObject.CompareTag("Obstaculo") || collision.gameObject.CompareTag("Enemy"))
        {
            
            ContactPoint2D puntoContacto = collision.GetContact(0);
            if (puntoContacto.normal.y >= -0.5f)
            { 
                RecibirDano(1); 
            }
        }
    }
    public void RecibirDano(int cantidad)
    {
        if (vidaActual <= 0) return;
        vidaActual -= cantidad;
        Debug.Log($"Jugador recibi� {cantidad} da�o, vida restante: {vidaActual}");
        if (vidaActual <= 0)
        {
            Morir();
        }
    }

    private void Morir()
    {
        Debug.Log("Jugador Muri�");
        OnMuerteJugador?.Invoke(); 
        animator.SetTrigger("Muerte"); 
        rb.linearVelocity = Vector2.zero; 
        GetComponent<Collider2D>().enabled = false; 
        this.enabled = false; 
        
    }
}