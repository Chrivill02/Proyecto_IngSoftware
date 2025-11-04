using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class TimedPlatform : MonoBehaviour
{
    [Header("Parámetros de Movimiento")]
    [SerializeField] private float velocidad = 2.5f;
    [SerializeField] private float alturaDeSubida = 8f;

    [Header("Spawn de Enemigo")]
    public GameObject enemigoPrefab;
    public Transform puntoSpawnEnemigo;

    // --- Variables privadas ---
    private Rigidbody2D rb;
    private Vector2 posicionInicial;
    private Vector2 posicionObjetivo;
    private bool puedeMoverse = false;
    private bool enemigoGenerado = false;

    // Guardaremos una referencia al enemigo que generemos
    private GameObject enemigoInstanciado;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;

        posicionInicial = transform.position;
        posicionObjetivo = new Vector2(posicionInicial.x, posicionInicial.y + alturaDeSubida);

        if (puntoSpawnEnemigo == null)
        {
            puntoSpawnEnemigo = transform;
        }
    }

    void FixedUpdate()
    {
        // --- NUEVA LÓGICA DE DETECCIÓN ---
        // Si el enemigo ya se generó, aún no nos podemos mover
        // Y la referencia al enemigo es 'null' (porque fue destruido)...
        if (enemigoGenerado && !puedeMoverse && enemigoInstanciado == null)
        {
            // ... ¡Activamos el movimiento!
            ActivarMovimiento();
        }
        // --- FIN DE LA NUEVA LÓGICA ---

        if (puedeMoverse)
        {
            Vector2 nuevaPosicion = Vector2.MoveTowards(rb.position, posicionObjetivo, velocidad * Time.fixedDeltaTime);
            rb.MovePosition(nuevaPosicion);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !enemigoGenerado)
        {
            enemigoGenerado = true;
            GenerarEnemigo();
        }
    }

    private void GenerarEnemigo()
    {
        if (enemigoPrefab == null) return;

        // Generamos el enemigo y guardamos la referencia en 'enemigoInstanciado'
        enemigoInstanciado = Instantiate(enemigoPrefab, puntoSpawnEnemigo.position, puntoSpawnEnemigo.rotation);

        // Ya no necesitamos la lógica específica de 'EnemigoSaltable'
        // ni la de los puntos de patrulla, ni la del panel.
    }


    // Esta función ahora solo se encarga de activar el movimiento
    public void ActivarMovimiento()
    {
        puedeMoverse = true;
    }
}