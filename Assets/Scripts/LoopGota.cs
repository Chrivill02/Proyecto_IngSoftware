using UnityEngine;

public class LoopGota : MonoBehaviour
{
    public float velocidad = 5f;   // velocidad de caída
    public float limiteY = -4f;    // altura donde desaparece
    public float inicioY = 60f;     // altura inicial para reaparecer
    public float intervalo = 5f;   // tiempo para reaparecer

    private float tiempoReaparecer = 0f;
    private float velocidadInicial;

    void Start()
    {
        // Guardamos la velocidad original
        velocidadInicial = velocidad;
    }

    void Update()
    {
        // Movimiento hacia abajo
        transform.Translate(Vector2.down * velocidad * Time.deltaTime);

        // Si llegó al límite
        if (transform.position.y <= limiteY)
        {
            // Cuenta tiempo hasta reaparecer
            tiempoReaparecer += Time.deltaTime;
            if (tiempoReaparecer >= intervalo)
            {
                transform.position = new Vector3(transform.position.x, inicioY, transform.position.z);
                tiempoReaparecer = 0f;
                velocidad = velocidadInicial; // Reiniciamos la velocidad
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Si toca el suelo o jugador, desaparece y empieza cuenta
        if (other.CompareTag("Suelo") || other.CompareTag("Personaje"))
        {
            transform.position = new Vector3(transform.position.x, limiteY, transform.position.z);
            tiempoReaparecer = 0f;
            velocidad = velocidadInicial; // Reiniciamos la velocidad
        }
    }
}
