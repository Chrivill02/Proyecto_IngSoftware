using UnityEngine;

public class PlataformaMovil : MonoBehaviour
{
    public Transform puntoA;
    public Transform puntoB;
    public float velocidad = 2f;
    public float tiempoEspera = 1f;

    private Vector3 posicionA;
    private Vector3 posicionB;
    private Vector3 destinoActual;
    private bool esperando = false;
    private float contadorEspera;

    void Start()
    {
        if (puntoA == null || puntoB == null)
        {
            Debug.LogError("ASIGNA puntoA y puntoB en el Inspector!");
            return;
        }

        // GUARDAR POSICIONES INICIALES FIJAS
        posicionA = puntoA.position;
        posicionB = puntoB.position;

        transform.position = posicionA;
        destinoActual = posicionB;
        contadorEspera = tiempoEspera;

        Debug.Log("PLATAFORMA INICIADA:");
        Debug.Log("Punto A FIJADO: " + posicionA);
        Debug.Log("Punto B FIJADO: " + posicionB);
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

                if (destinoActual == posicionA)
                {
                    destinoActual = posicionB;
                    Debug.Log(" Cambiando: A  B (" + posicionB + ")");
                }
                else
                {
                    destinoActual = posicionA;
                    Debug.Log(" Cambiando: B  A (" + posicionA + ")");
                }
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                destinoActual,
                velocidad * Time.deltaTime
            );

            if (Vector3.Distance(transform.position, destinoActual) < 0.1f)
            {
                esperando = true;
                Debug.Log(" Llegamos! Esperando...");
            }
        }
    }

    void OnDrawGizmos()
    {
        if (puntoA != null && puntoB != null)
        {
            Vector3 puntoAVisual = Application.isPlaying ? posicionA : puntoA.position;
            Vector3 puntoBVisual = Application.isPlaying ? posicionB : puntoB.position;

            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(puntoAVisual, puntoBVisual);

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(puntoAVisual, 0.3f);

            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(puntoBVisual, 0.3f);
        }
    }
}