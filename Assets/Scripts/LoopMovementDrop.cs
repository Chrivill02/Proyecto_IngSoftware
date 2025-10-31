using UnityEngine;

public class MovimientoCiclicoGota : MonoBehaviour
{
    // --- Variables Configurables desde el Inspector ---

    [Tooltip("La velocidad a la que la gota cae.")]
    public float velocidadCaida = 5f;

    [Tooltip("La velocidad a la que la gota regresa a su punto de inicio.")]
    public float velocidadRegreso = 3f;

    [Tooltip("La altura máxima o punto inicial desde donde empieza a caer.")]
    public float puntoInicialY = 10f;

    [Tooltip("La altura mínima a la que llegará la gota antes de regresar.")]
    public float puntoMinimoY = -5f;

    // --- Variables Internas del Script ---
    private Vector3 puntoInicial;
    private Vector3 puntoMinimo;
    private bool estaCayendo = true; // Controla la dirección del movimiento

    /// <summary>
    /// Se ejecuta al iniciar el juego para configurar las posiciones.
    /// </summary>
    void Start()
    {
        // Define los puntos de inicio y fin del movimiento
        puntoInicial = new Vector3(transform.position.x, puntoInicialY, transform.position.z);
        puntoMinimo = new Vector3(transform.position.x, puntoMinimoY, transform.position.z);

        // Posiciona la gota en su punto de partida
        transform.position = puntoInicial;
    }

    /// <summary>
    /// Se ejecuta en cada fotograma para manejar la lógica de movimiento.
    /// </summary>
    void Update()
    {
        // Si la gota debe ir hacia abajo
        if (estaCayendo)
        {
            // Mueve el objeto hacia el punto mínimo
            transform.position = Vector3.MoveTowards(transform.position, puntoMinimo, velocidadCaida * Time.deltaTime);

            // Si llega al destino, invierte la dirección
            if (Vector3.Distance(transform.position, puntoMinimo) < 0.01f)
            {
                estaCayendo = false;
            }
        }
        // Si la gota debe ir hacia arriba
        else
        {
            // Mueve el objeto hacia el punto inicial
            transform.position = Vector3.MoveTowards(transform.position, puntoInicial, velocidadRegreso * Time.deltaTime);

            // Si llega al origen, reinicia el ciclo
            if (Vector3.Distance(transform.position, puntoInicial) < 0.01f)
            {
                estaCayendo = true;
            }
        }
    }
}