using System.Collections;
using UnityEngine;

// Asegura que el objeto siempre tendrá estos componentes 2D.
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class TimedPlatform : MonoBehaviour
{
    [Header("Parámetros de Movimiento")]
    [Tooltip("La velocidad con la que se moverá la plataforma.")]
    [SerializeField] private float velocidad = 2.5f;

    [Tooltip("Cuántas unidades subirá la plataforma desde su punto de inicio.")]
    [SerializeField] private float alturaDeSubida = 8f;

    [Tooltip("Segundos a esperar antes de subir, desde que el jugador la toca.")]
    [SerializeField] private float retrasoAlTocar = 5f;

    // Variables internas para controlar el estado y movimiento.
    private Rigidbody2D rb;
    private Vector2 posicionInicial;
    private Vector2 posicionObjetivo;
    private bool puedeMoverse = false;
    private bool contadorIniciado = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true; // Permite mover la plataforma por script sin físicas extrañas.

        // Guarda la posición inicial y calcula el destino final.
        posicionInicial = transform.position;
        posicionObjetivo = new Vector2(posicionInicial.x, posicionInicial.y + alturaDeSubida);
    }

    void FixedUpdate()
    {
        // El movimiento se ejecuta en FixedUpdate por estar basado en físicas (Rigidbody).
        if (puedeMoverse)
        {
            Vector2 nuevaPosicion = Vector2.MoveTowards(rb.position, posicionObjetivo, velocidad * Time.fixedDeltaTime);
            rb.MovePosition(nuevaPosicion);
        }
    }

    // Se activa cuando otro Collider2D entra en contacto con este.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Si es el jugador y el contador no ha empezado, inicia el proceso.
        if (collision.gameObject.CompareTag("Player") && !contadorIniciado)
        {
            contadorIniciado = true;
            StartCoroutine(ActivarMovimientoConRetraso());
        }
    }

    // Corrutina que gestiona el tiempo de espera.
    private IEnumerator ActivarMovimientoConRetraso()
    {
        // Pausa la ejecución por el tiempo definido.
        yield return new WaitForSeconds(retrasoAlTocar);

        // Una vez terminada la espera, permite que la plataforma se mueva.
        puedeMoverse = true;
    }
}