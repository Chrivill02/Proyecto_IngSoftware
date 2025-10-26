using UnityEngine;
public class EnemigoSaltable : BaseEnemy
{ // Hereda de BaseEnemy
    [Header("Movimiento")]
    [SerializeField] private float speed = 1.5f;
    [SerializeField] private float fuerzaReboteJugador = 8f;
    public Transform[] puntosDePatrulla; // Asignar en Inspector o por c�digo

    
    private Rigidbody2D rb;
    private int currentPoint = 0;
    private bool isMoving = false;

    protected override void Awake()
    { 
        base.Awake(); // Llama a Awake de BaseEnemy
        rb = GetComponent<Rigidbody2D>();
    }

    protected void Start()
    { 
        
        if (puntosDePatrulla != null && puntosDePatrulla.Length >= 2)
        {
            isMoving = true;
        }
        else
        {
            Debug.LogWarning("EnemigoSaltable no tiene puntos de patrulla.", this);
        }
    }

    void Update() { Girar(); } // Girar basado en velocidad

    void FixedUpdate() { if (isMoving && vidaActual > 0) { Patrullar(); } }

    private void Patrullar() { /* ... tu l�gica de patrulla ... */ }
    private void Girar() { /* ... tu l�gica de giro ... */ }

    // OnCollisionEnter2D para da�ar al jugador o recibir da�o
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (vidaActual <= 0) return;
        if (collision.gameObject.CompareTag("Player"))
        {
            ContactPoint2D puntoContacto = collision.GetContact(0);
            if (puntoContacto.normal.y < -0.5f)
            { // Jugador salt� encima
                RecibirDano(1); // Llama al m�todo heredado
                                // Hacer rebotar al jugador
                Rigidbody2D rbJugador = collision.gameObject.GetComponent<Rigidbody2D>();
                if (rbJugador != null)
                {
                    rbJugador.linearVelocity = new Vector2(rbJugador.linearVelocity.x, 0);
                    rbJugador.AddForce(Vector2.up * fuerzaReboteJugador, ForceMode2D.Impulse);
                }
            }
            else
            { // Enemigo toca al jugador
                Damageable playerDamageable = collision.gameObject.GetComponent<Damageable>();
                playerDamageable?.RecibirDano(1); // Da�a al jugador
            }
        }
    }
    // Quitar RecibirDano (est� en BaseEnemy)
    // Quitar Morir (est� en BaseEnemy, la notificaci�n a TimedPlatform se har� por evento)
}

