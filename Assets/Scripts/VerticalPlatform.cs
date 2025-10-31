using UnityEngine;

public class PlataformaVertical : MonoBehaviour
{
    public Transform puntoArriba;
    public Transform puntoAbajo;
    public float velocidad = 2f;
    public float tiempoEspera = 1f;

    private Rigidbody2D rb;
    private Vector3 posicionArriba;
    private Vector3 posicionAbajo;
    private Vector3 destinoActual;
    private bool esperando = false;
    private float contadorEspera;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        posicionArriba = puntoArriba.position;
        posicionAbajo = puntoAbajo.position;

        transform.position = posicionAbajo;
        destinoActual = posicionArriba;
        contadorEspera = tiempoEspera;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, destinoActual) < 0.1f)
        {
            esperando = true;
        }

        if (esperando)
        {
            contadorEspera -= Time.deltaTime;
            if (contadorEspera <= 0)
            {
                esperando = false;
                contadorEspera = tiempoEspera;

                if (destinoActual == posicionArriba)
                {
                    destinoActual = posicionAbajo;
                }
                else
                {
                    destinoActual = posicionArriba;
                }
            }
        }
    }

    void FixedUpdate()
    {
        if (!esperando)
        {
            Vector2 nuevaPosicion = Vector2.MoveTowards(
                rb.position,
                destinoActual,
                velocidad * Time.fixedDeltaTime
            );

            rb.MovePosition(nuevaPosicion);
        }
    }

}