using UnityEngine;

public class PlataformaVertical : MonoBehaviour
{
    public Transform puntoArriba;
    public Transform puntoAbajo;
    public float velocidad = 2f;
    public float tiempoEspera = 1f;

    private Vector3 posicionArriba;
    private Vector3 posicionAbajo;
    private Vector3 destinoActual;
    private bool esperando = false;
    private float contadorEspera;
    private bool moviendoHaciaArriba = false;

    void Start()
    {
        if (puntoArriba == null || puntoAbajo == null)
        {
            Debug.LogError("ASIGNA puntoArriba y puntoAbajo en el Inspector!");
            return;
        }

        // GUARDAR POSICIONES INICIALES FIJAS
        posicionArriba = puntoArriba.position;
        posicionAbajo = puntoAbajo.position;

        // Comenzar en la posición de abajo y moverse hacia arriba
        transform.position = posicionAbajo;
        destinoActual = posicionArriba;
        moviendoHaciaArriba = true;
        contadorEspera = tiempoEspera;

        Debug.Log("PLATAFORMA VERTICAL INICIADA:");
        Debug.Log("Punto Arriba: " + posicionArriba);
        Debug.Log("Punto Abajo: " + posicionAbajo);
    }

    void Update()
    {
        if (esperando)
        {
            contadorEspera -= Time.deltaTime;
            if (contadorEspera <= 0)
            {
                esperando = false;
                contadorEspera = tiempoEspera;

                // CAMBIAR DIRECCIÓN
                if (moviendoHaciaArriba)
                {
                    destinoActual = posicionAbajo;
                    moviendoHaciaArriba = false;
                    Debug.Log(" Cambiando: ARRIBA  ABAJO");
                }
                else
                {
                    destinoActual = posicionArriba;
                    moviendoHaciaArriba = true;
                    Debug.Log(" Cambiando: ABAJO  ARRIBA");
                }
            }
        }
        else
        {
            // MOVIMIENTO VERTICAL
            transform.position = Vector3.MoveTowards(
                transform.position,
                destinoActual,
                velocidad * Time.deltaTime
            );

            // VERIFICAR SI LLEGÓ AL DESTINO
            if (Vector3.Distance(transform.position, destinoActual) < 0.1f)
            {
                esperando = true;
                Debug.Log(" Llegamos al punto " + (moviendoHaciaArriba ? "ARRIBA" : "ABAJO"));
            }
        }
    }

    // Para que el jugador se mueva con la plataforma
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
            Debug.Log("Jugador subió a la plataforma vertical");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
            Debug.Log("Jugador bajó de la plataforma vertical");
        }
    }

    void OnDrawGizmos()
    {
        if (puntoArriba != null && puntoAbajo != null)
        {
            Vector3 puntoArribaVisual = Application.isPlaying ? posicionArriba : puntoArriba.position;
            Vector3 puntoAbajoVisual = Application.isPlaying ? posicionAbajo : puntoAbajo.position;

            // Línea vertical entre puntos
            Gizmos.color = Color.magenta;
            Gizmos.DrawLine(puntoArribaVisual, puntoAbajoVisual);

            // Punto ARRIBA - AZUL
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(puntoArribaVisual, 0.3f);

            // Punto ABAJO - AMARILLO
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(puntoAbajoVisual, 0.3f);

            // Flecha de dirección actual
            if (Application.isPlaying)
            {
                Gizmos.color = moviendoHaciaArriba ? Color.blue : Color.yellow;
                Vector3 direccion = (destinoActual - transform.position).normalized;
                Gizmos.DrawRay(transform.position, direccion * 1f);
            }
        }
    }
}