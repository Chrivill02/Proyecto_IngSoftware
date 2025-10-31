using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemigoSaltable : MonoBehaviour
{
    [Header("Estadísticas de Combate")]
    public int vida = 3;
    public float fuerzaReboteJugador = 10f;

    [Header("Parámetros de Movimiento")]
    public float speed = 1.5f; 

    
    [HideInInspector] 
    public Transform[] puntosDePatrulla;

   
    public TimedPlatform plataformaQueMeInvoco;
    private Rigidbody2D rb;
    private int currentPoint = 0;
    private bool isMoving = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        
        if (puntosDePatrulla != null && puntosDePatrulla.Length >= 2)
        {
            isMoving = true;
        }
        else
        {
            Debug.LogWarning("EnemigoSalteble no recibió puntos de patrulla. No se moverá.");
        }
    }

    void Update()
    {
        
        Girar();
    }

    void FixedUpdate()
    {
        
        if (isMoving)
        {
            Patrullar();
        }
    }

    private void Patrullar()
    {
        
        Transform puntoObjetivo = puntosDePatrulla[currentPoint];

        
        float direccionHorizontal = Mathf.Sign(puntoObjetivo.position.x - transform.position.x);

        
        rb.linearVelocity = new Vector2(direccionHorizontal * speed, rb.linearVelocity.y);

        
        if (Mathf.Abs(transform.position.x - puntoObjetivo.position.x) < 0.2f)
        {
            
            currentPoint++;
            if (currentPoint >= puntosDePatrulla.Length)
            {
                currentPoint = 0; 
            }
        }
    }

    private void Girar()
    {
        
        if (Mathf.Abs(rb.linearVelocity.x) > 0.1f)
        {
            Vector3 escalaActual = transform.localScale;
            if (rb.linearVelocity.x < 0) 
            {
                escalaActual.x = -Mathf.Abs(escalaActual.x);
            }
            else 
            {
                escalaActual.x = Mathf.Abs(escalaActual.x);
            }
            transform.localScale = escalaActual;
        }
    }

   

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ContactPoint2D puntoContacto = collision.GetContact(0);

            if (puntoContacto.normal.y < -0.5f)
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

    private void RecibirDano(GameObject jugador)
    {
        vida--;

        Rigidbody2D rbJugador = jugador.GetComponent<Rigidbody2D>();
        if (rbJugador != null)
        {
            rbJugador.linearVelocity = new Vector2(rbJugador.linearVelocity.x, 0);
            rbJugador.AddForce(Vector2.up * fuerzaReboteJugador, ForceMode2D.Impulse);
        }

        if (vida <= 0)
        {
            Morir();
        }
    }

    private void Morir()
    {
        if (plataformaQueMeInvoco != null)
        {
            plataformaQueMeInvoco.ActivarMovimiento();
        }

        Destroy(gameObject);
    }
}