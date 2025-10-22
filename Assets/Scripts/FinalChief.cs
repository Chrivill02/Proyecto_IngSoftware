using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class JefeFinal : MonoBehaviour
{
   
    private enum EstadoJefe
    {
        Patrullando,   
        Embestida,      
        Saltando,       
        Cayendo,        
        Spawneando,     
        Cooldown,       
        Invulnerable    
    }

    [Header("Estadísticas")]
    public int vida = 10;
    public float fuerzaReboteJugador = 12f;

    [Header("Movimiento")]
    public float velocidadPatrulla = 2f;
    public float velocidadEmbestida = 8f; 
    public Transform[] puntosDePatrulla;

    [Header("Ataque de Salto (Slam)")]
    public float fuerzaSalto = 15f;
    public float fuerzaCaida = 25f;

    [Header("Spawn de Esbirros")]
    public GameObject prefabMiniEnemigo; 
    public Transform puntoDeSpawn;

    [Header("Loot")]
    public GameObject prefabLlave; 


    [Header("Referencias")]
    public Transform jugadorTransform;
    public Transform detectorSuelo;
    public float radioDetector = 0.1f;
    public LayerMask layerSuelo;

    [Header("Tiempos de IA")]
    public float tiempoPatrulla = 5f;
    public float tiempoCooldown = 2f;

   
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private EstadoJefe estadoActual;
    private int currentPoint = 0;
    private bool estaInvulnerable = false;
    private float temporizadorEstado = 0f; 
    private float direccionEmbestida = 1f; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

       
        if (jugadorTransform == null)
        {
            jugadorTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }

        
        estadoActual = EstadoJefe.Patrullando;
        temporizadorEstado = tiempoPatrulla;
    }

    void Update()
    {
       
        if (estaInvulnerable || vida <= 0) return;

       
        temporizadorEstado -= Time.deltaTime;

       
        Girar();

        if (temporizadorEstado <= 0)
        {
            
            if (estadoActual == EstadoJefe.Cooldown)
            {
               
                ElegirSiguienteAccion();
            }
            else
            {
                
                IniciarCooldown();
            }
        }

        
        if (estadoActual == EstadoJefe.Saltando && rb.linearVelocity.y < 0.1f)
        {
            
            estadoActual = EstadoJefe.Cayendo;
          
            float dir = Mathf.Sign(jugadorTransform.position.x - transform.position.x);
            rb.linearVelocity = new Vector2(dir * velocidadPatrulla, rb.linearVelocity.y);
        }
    }

    void FixedUpdate()
    {
        
        if (estaInvulnerable || vida <= 0)
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y); 
            return;
        }

        
        switch (estadoActual)
        {
            case EstadoJefe.Patrullando:
                Patrullar();
                break;
            case EstadoJefe.Embestida:
                Embestir();
                break;
            case EstadoJefe.Cayendo:
                AplicarFuerzaCaida();
                if (EstaEnSuelo()) 
                {
                    IniciarCooldown();
                   
                }
                break;
        }
    }

    

    void IniciarCooldown()
    {
        estadoActual = EstadoJefe.Cooldown;
        temporizadorEstado = tiempoCooldown;
        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y); 
    }

    void ElegirSiguienteAccion()
    {
        
        int accion = Random.Range(0, 3);

        switch (accion)
        {
            case 0: 
                estadoActual = EstadoJefe.Embestida;
                temporizadorEstado = 2f; 
                
                direccionEmbestida = Mathf.Sign(jugadorTransform.position.x - transform.position.x);
                
                break;
            case 1: 
                estadoActual = EstadoJefe.Saltando;
                rb.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
                temporizadorEstado = 5f;
                break;
            case 2: 
                estadoActual = EstadoJefe.Spawneando;
                temporizadorEstado = 1.5f; 
                rb.linearVelocity = Vector2.zero;
                SpawnMinion();
                
                break;
        }
    }

   

    private void Patrullar()
    {
        
        if (puntosDePatrulla == null || puntosDePatrulla.Length < 2) return;

        Transform puntoObjetivo = puntosDePatrulla[currentPoint];
        float direccionHorizontal = Mathf.Sign(puntoObjetivo.position.x - transform.position.x);

        rb.linearVelocity = new Vector2(direccionHorizontal * velocidadPatrulla, rb.linearVelocity.y);

        if (Mathf.Abs(transform.position.x - puntoObjetivo.position.x) < 0.2f)
        {
            currentPoint = (currentPoint + 1) % puntosDePatrulla.Length;
        }
    }

    private void Embestir()
    {
       
        rb.linearVelocity = new Vector2(direccionEmbestida * velocidadEmbestida, rb.linearVelocity.y);
    }

    private void AplicarFuerzaCaida()
    {
       
        rb.AddForce(Vector2.down * fuerzaCaida, ForceMode2D.Force);
    }

    private void SpawnMinion()
    {
        
        Instantiate(prefabMiniEnemigo, puntoDeSpawn.position, Quaternion.identity);
    }

    private void Girar()
    {
        
        if (Mathf.Abs(rb.linearVelocity.x) > 0.1f)
        {
            Vector3 escalaActual = transform.localScale;
            escalaActual.x = Mathf.Abs(escalaActual.x) * Mathf.Sign(rb.linearVelocity.x);
            transform.localScale = escalaActual;
        }
    }

    private bool EstaEnSuelo()
    {
       
        return Physics2D.OverlapCircle(detectorSuelo.position, radioDetector, layerSuelo) != null;
    }

    

    private void OnCollisionEnter2D(Collision2D collision)
    {
       
        if (vida <= 0) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            ContactPoint2D puntoContacto = collision.GetContact(0);

            
            if (puntoContacto.normal.y < -0.5f && !estaInvulnerable)
            {
                RecibirDano(collision.gameObject);
            }
            else 
            {
               
                Jugador scriptJugador = collision.gameObject.GetComponent<Jugador>();
                if (scriptJugador != null && scriptJugador.gameManager != null)
                {
                    scriptJugador.gameManager.gameOver = true;
                }
            }
        }
    }

   
    public void RecibirDano(GameObject jugador)
    {
        if (estaInvulnerable) return;

        vida--;
        StartCoroutine(PeriodoDeInvulnerabilidad());

        
        if (jugador != null)
        {
            Rigidbody2D rbJugador = jugador.GetComponent<Rigidbody2D>();
            if (rbJugador != null)
            {
                rbJugador.linearVelocity = new Vector2(rbJugador.linearVelocity.x, 0);
                rbJugador.AddForce(Vector2.up * fuerzaReboteJugador, ForceMode2D.Impulse);
            }
        }
       

        if (vida <= 0)
        {
            Morir();
        }
    }

    
    private IEnumerator PeriodoDeInvulnerabilidad()
    {
        estaInvulnerable = true;
        estadoActual = EstadoJefe.Invulnerable; 

        
        for (int i = 0; i < 5; i++)
        {
            sr.color = new Color(1f, 1f, 1f, 0.5f); 
            yield return new WaitForSeconds(0.1f);
            sr.color = Color.white;
            yield return new WaitForSeconds(0.1f);
        }

        estaInvulnerable = false;
        IniciarCooldown(); 
    }

    private void Morir()
    {
        
        StopAllCoroutines();
        rb.linearVelocity = Vector2.zero;
        GetComponent<Collider2D>().enabled = false; 
        this.enabled = false; 

        if (prefabLlave != null)
        {
            Instantiate(prefabLlave, transform.position, Quaternion.identity);
        }

        Debug.Log("¡JEFE DERROTADO!");

  
        Destroy(gameObject, 2f); 
    }
}

