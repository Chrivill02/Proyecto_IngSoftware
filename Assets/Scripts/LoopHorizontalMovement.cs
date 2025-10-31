using UnityEngine;

public class MovimientoHorizontalCiclico : MonoBehaviour
{
    // --- Variables Configurables desde el Inspector ---

    [Tooltip("Velocidad del objeto al moverse hacia el punto final.")]
    public float velocidadIda = 5f;

    [Tooltip("Velocidad del objeto al regresar al punto inicial.")]
    public float velocidadRegreso = 5f;

    [Tooltip("Posición X inicial desde donde parte el objeto.")]
    public float puntoInicialX = -8f;

    [Tooltip("Posición X final a la que llegará el objeto antes de regresar.")]
    public float puntoFinalX = 8f;

    // --- Variables Internas del Script ---
    private Vector3 puntoInicial;
    private Vector3 puntoFinal;
    private bool moviendoHaciaElFinal = true; // Controla la dirección del movimiento

    /// <summary>
    /// Se ejecuta al iniciar para configurar las posiciones de inicio y fin.
    /// </summary>
    void Start()
    {
        // Define los puntos de inicio y fin del recorrido horizontal
        puntoInicial = new Vector3(puntoInicialX, transform.position.y, transform.position.z);
        puntoFinal = new Vector3(puntoFinalX, transform.position.y, transform.position.z);

        // Coloca el objeto en su punto de partida
        transform.position = puntoInicial;
    }

    /// <summary>
    /// Se ejecuta en cada fotograma para manejar el movimiento.
    /// </summary>
    void Update()
    {
        // Verifica si el objeto se está moviendo hacia el punto final
        if (moviendoHaciaElFinal)
        {
            // Mueve el objeto hacia el punto final
            transform.position = Vector3.MoveTowards(transform.position, puntoFinal, velocidadIda * Time.deltaTime);

            // Si llega a su destino, invierte la dirección
            if (Vector3.Distance(transform.position, puntoFinal) < 0.01f)
            {
                moviendoHaciaElFinal = false;
            }
        }
        // Si no, se está moviendo de regreso al inicio
        else
        {
            // Mueve el objeto hacia el punto inicial
            transform.position = Vector3.MoveTowards(transform.position, puntoInicial, velocidadRegreso * Time.deltaTime);

            // Si llega al origen, reinicia el ciclo
            if (Vector3.Distance(transform.position, puntoInicial) < 0.01f)
            {
                moviendoHaciaElFinal = true;
            }
        }
    }
}