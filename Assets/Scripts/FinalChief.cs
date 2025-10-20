using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class JefeFinal : MonoBehaviour
{
    // --- Estados de la IA ---
    // Usamos una "máquina de estados" para controlar qué hace el jefe en cada momento.
    private enum EstadoJefe
    {
        Patrullando,    // Moviéndose de lado a lado
        Embestida,      // Corriendo rápido hacia el jugador
        Saltando,       // Preparando el ataque de caída
        Cayendo,        // Cayendo con fuerza
        Spawneando,     // Generando un mini enemigo
        Cooldown,       // Descansando entre acciones
        Invulnerable    // Recibiendo daño
    }

    [Header("Estadísticas")]
    public int vida = 10;
    public float fuerzaReboteJugador = 12f;

    [Header("Movimiento")]
    public float velocidadPatrulla = 2f;
    public float velocidadEmbestida = 8f; // Para el ataque de "correr"
    public Transform[] puntosDePatrulla;

    [Header("Ataque de Salto (Slam)")]
    public float fuerzaSalto = 15f;
    public float fuerzaCaida = 25f; // Fuerza extra hacia abajo

    [Header("Spawn de Esbirros")]
    public GameObject prefabMiniEnemigo; // Arrastra aquí tu prefab del EnemigoSaltable
    public Transform puntoDeSpawn; // Un punto (GameObject vacío) donde aparecerá el esbirro
    

    [Header("Referencias")]
    public Transform jugadorTransform;
    public Transform detectorSuelo;
    public float radioDetector = 0.1f;
    public LayerMask layerSuelo;

    [Header("Tiempos de IA")]
    public float tiempoPatrulla = 5f;
    public float tiempoCooldown = 2f;

    // --- Variables Privadas ---
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private EstadoJefe estadoActual;
    private int currentPoint = 0;
    private bool estaInvulnerable = false;
    private float temporizadorEstado = 0f; // Controla la duración de cada estado
    private float direccionEmbestida = 1f; // Dirección de la embestida

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        // Buscar al jugador por Tag si no se asignó en el inspector
        if (jugadorTransform == null)
        {
            jugadorTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }

        // Empezar patrullando
        estadoActual = EstadoJefe.Patrullando;
        temporizadorEstado = tiempoPatrulla;
    }

    void Update()
    {
        // Si está invulnerable o muerto, no hacer nada
        if (estaInvulnerable || vida <= 0) return;

        // Reducir el temporizador del estado actual
        temporizadorEstado -= Time.deltaTime;

        // Lógica de la IA
        Girar();

        if (temporizadorEstado <= 0)
        {
            // Si se acabó el tiempo de la acción actual (o el cooldown)...
            if (estadoActual == EstadoJefe.Cooldown)
            {
                // ...elegir una nueva acción.
                ElegirSiguienteAccion();
            }
            else
            {
                // ...si no, entrar en cooldown (descansar).
                IniciarCooldown();
            }
        }

        // Lógica de estados que no es de física
        if (estadoActual == EstadoJefe.Saltando && rb.linearVelocity.y < 0.1f)
        {
            // Si terminó de subir (Saltando) y empieza a caer, cambia a estado "Cayendo"
            estadoActual = EstadoJefe.Cayendo;
            // Opcional: moverse hacia el jugador mientras cae
            float dir = Mathf.Sign(jugadorTransform.position.x - transform.position.x);
            rb.linearVelocity = new Vector2(dir * velocidadPatrulla, rb.linearVelocity.y);
        }
    }

    void FixedUpdate()
    {
        // Si está invulnerable o muerto, no mover
        if (estaInvulnerable || vida <= 0)
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y); // Frena si está muerto/invulnerable
            return;
        }

        // Lógica de física según el estado
        switch (estadoActual)
        {
            case EstadoJefe.Patrullando:
                Patrullar();
                break;
            case EstadoJefe.Embestida:
                Embestir();
                break;
            case EstadoJefe.Cayendo:
                AplicarFuerzaCaida();
                if (EstaEnSuelo()) // Si toca el suelo cayendo, entra en cooldown
                {
                    IniciarCooldown();
                    // Opcional: Aquí podrías añadir un efecto (partículas, sonido) de "Golpe contra el suelo"
                }
                break;
        }
    }

    // --- Lógica de la IA ---

    void IniciarCooldown()
    {
        estadoActual = EstadoJefe.Cooldown;
        temporizadorEstado = tiempoCooldown;
        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y); // Frenar
    }

    void ElegirSiguienteAccion()
    {
        // Elige una acción aleatoria (0, 1, o 2)
        int accion = Random.Range(0, 3);

        switch (accion)
        {
            case 0: // EMVESTIDA
                estadoActual = EstadoJefe.Embestida;
                temporizadorEstado = 2f; // Duración de la embestida
                // Decide la dirección de la embestida (hacia el jugador)
                direccionEmbestida = Mathf.Sign(jugadorTransform.position.x - transform.position.x);
                // Opcional: Añadir un "aviso" visual o de sonido aquí
                break;
            case 1: // SALTO Y CAÍDA
                estadoActual = EstadoJefe.Saltando;
                rb.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
                temporizadorEstado = 5f; // Tiempo máximo para la maniobra (por seguridad)
                break;
            case 2: // SPAWN DE ESBIRRO
                estadoActual = EstadoJefe.Spawneando;
                temporizadorEstado = 1.5f; // Tiempo que tarda en spawnear (para animación)
                rb.linearVelocity = Vector2.zero; // Quedarse quieto mientras spawnea
                SpawnMinion();
                // Al acabar el tiempo, entrará en Cooldown automáticamente
                break;
        }
    }

    // --- Métodos de Comportamiento ---

    private void Patrullar()
    {
        // Lógica de patrulla (idéntica a tu EnemigoSaltable)
        if (puntosDePatrulla == null || puntosDePatrulla.Length < 2) return;

        Transform puntoObjetivo = puntosDePatrulla[currentPoint];
        float direccionHorizontal = Mathf.Sign(puntoObjetivo.position.x - transform.position.x);

        rb.linearVelocity = new Vector2(direccionHorizontal * velocidadPatrulla, rb.linearVelocity.y);

        if (Mathf.Abs(transform.position.x - puntoObjetivo.position.x) < 0.2f)
        {
            currentPoint = (currentPoint + 1) % puntosDePatrulla.Length;
        }
    }

    private void Embestir()
    {
        // Simplemente se mueve rápido en la dirección decidida
        rb.linearVelocity = new Vector2(direccionEmbestida * velocidadEmbestida, rb.linearVelocity.y);
    }

    private void AplicarFuerzaCaida()
    {
        // Añade fuerza hacia abajo para que la caída sea más rápida y "pesada"
        rb.AddForce(Vector2.down * fuerzaCaida, ForceMode2D.Force);
    }

    private void SpawnMinion()
    {
        // Simplemente instancia el prefab en el punto de spawn.
        // El script "MinionSigueJugador" se encargará del resto.
        Instantiate(prefabMiniEnemigo, puntoDeSpawn.position, Quaternion.identity);
    }

    private void Girar()
    {
        // Gira el sprite basado en la velocidad (idéntico a tu EnemigoSaltable)
        if (Mathf.Abs(rb.linearVelocity.x) > 0.1f)
        {
            Vector3 escalaActual = transform.localScale;
            // Forma más corta de escribir la lógica de giro:
            escalaActual.x = Mathf.Abs(escalaActual.x) * Mathf.Sign(rb.linearVelocity.x);
            transform.localScale = escalaActual;
        }
    }

    private bool EstaEnSuelo()
    {
        // Lógica de detección de suelo (idéntica a tu script Jugador)
        return Physics2D.OverlapCircle(detectorSuelo.position, radioDetector, layerSuelo) != null;
    }

    // --- Daño y Muerte ---

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Si ya está muerto, no hacer nada
        if (vida <= 0) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            ContactPoint2D puntoContacto = collision.GetContact(0);

            // Si el jugador le salta encima (normal.y es "hacia abajo")
            if (puntoContacto.normal.y < -0.5f && !estaInvulnerable)
            {
                RecibirDano(collision.gameObject);
            }
            else // Si el jefe toca al jugador por el lado o por abajo
            {
                // Hacemos Game Over
                Jugador scriptJugador = collision.gameObject.GetComponent<Jugador>();
                if (scriptJugador != null && scriptJugador.gameManager != null)
                {
                    scriptJugador.gameManager.gameOver = true;
                }
            }
        }
    }

    // En JefeFinal.cs
    public void RecibirDano(GameObject jugador)
    {
        if (estaInvulnerable) return;

        vida--;
        StartCoroutine(PeriodoDeInvulnerabilidad());

        // --- ¡AQUÍ ESTÁ EL ARREGLO! ---
        // Solo rebotamos al jugador si no es null (es decir, si no fue una bala)
        if (jugador != null)
        {
            Rigidbody2D rbJugador = jugador.GetComponent<Rigidbody2D>();
            if (rbJugador != null)
            {
                rbJugador.linearVelocity = new Vector2(rbJugador.linearVelocity.x, 0);
                rbJugador.AddForce(Vector2.up * fuerzaReboteJugador, ForceMode2D.Impulse);
            }
        }
        // --- FIN DEL ARREGLO ---

        if (vida <= 0)
        {
            Morir();
        }
    }

    // Corutina para parpadear y ser invulnerable
    private IEnumerator PeriodoDeInvulnerabilidad()
    {
        estaInvulnerable = true;
        estadoActual = EstadoJefe.Invulnerable; // Pausa la IA

        // Parpadeo (5 veces)
        for (int i = 0; i < 5; i++)
        {
            sr.color = new Color(1f, 1f, 1f, 0.5f); // Transparente
            yield return new WaitForSeconds(0.1f);
            sr.color = Color.white; // Opaco
            yield return new WaitForSeconds(0.1f);
        }

        estaInvulnerable = false;
        IniciarCooldown(); // Forzar un cooldown después de ser golpeado
    }

    private void Morir()
    {
        // Detener toda la lógica
        StopAllCoroutines();
        rb.linearVelocity = Vector2.zero;
        GetComponent<Collider2D>().enabled = false; // Desactiva colisiones
        this.enabled = false; // Desactiva el script

        // Opcional: Iniciar animación de muerte
        Debug.Log("¡JEFE DERROTADO!");

        // Destruir el objeto
        Destroy(gameObject, 2f); // Espera 2 segundos (para animación de muerte)
    }
}

