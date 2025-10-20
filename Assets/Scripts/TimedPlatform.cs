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

    [Header("Patrulla del Enemigo")]
    public Transform[] puntosDePatrullaEnemigo;

    [Header("UI de Instrucción")]
    [Tooltip("Arrastra aquí el 'PanelInstruccion' que creaste en el Canvas")]
    public GameObject MJInstructionPanel; 


    private Rigidbody2D rb;
    private Vector2 posicionInicial;
    private Vector2 posicionObjetivo;
    private bool puedeMoverse = false;
    private bool enemigoGenerado = false;

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

  
        if (MJInstructionPanel != null)
        {
            MJInstructionPanel.SetActive(false);
        }
    }

    void FixedUpdate()
    {
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

        GameObject enemigoGO = Instantiate(enemigoPrefab, puntoSpawnEnemigo.position, puntoSpawnEnemigo.rotation);

        EnemigoSaltable scriptEnemigo = enemigoGO.GetComponent<EnemigoSaltable>();
        if (scriptEnemigo != null)
        {
            scriptEnemigo.plataformaQueMeInvoco = this;
            scriptEnemigo.puntosDePatrulla = this.puntosDePatrullaEnemigo;
        }

     
        if (MJInstructionPanel != null)
        {
            MJInstructionPanel.SetActive(true);
        }
    }

 
    public void ActivarMovimiento()
    {
        puedeMoverse = true;

        if (MJInstructionPanel != null)
        {
            MJInstructionPanel.SetActive(false);
        }
    }
}