using UnityEngine;
using System;

public class TimedPlatform : MonoBehaviour
{

    [Header("Parámetros de Movimiento")]
    [SerializeField] private float velocidad = 2.5f;
    [SerializeField] private float alturaDeSubida = 8f;


    [Header("Spawn de Enemigo")]
    [SerializeField] private GameObject enemigoPrefab; 
    [SerializeField] private Transform puntoSpawnEnemigo; 

    [Header("Patrulla del Enemigo")]
    [SerializeField] private Transform[] puntosDePatrullaEnemigo;

    private Rigidbody2D rb; 
    private Vector2 posicionInicial;
    private Vector2 posicionObjetivo;
    private bool puedeMoverse = false; 
    private bool enemigoGenerado = false;
    private BaseEnemy enemigoInstancia;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
        if (rb != null) rb.isKinematic = true; 

        posicionInicial = transform.position;
        posicionObjetivo = new Vector2(posicionInicial.x, posicionInicial.y + alturaDeSubida);

        if (puntoSpawnEnemigo == null)
        {
            puntoSpawnEnemigo = transform;
        }

 
    }

    void FixedUpdate()
    {
        if (puedeMoverse && rb != null)
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
        enemigoInstancia = enemigoGO.GetComponent<BaseEnemy>();

        if (enemigoInstancia != null)
        {
            enemigoInstancia.OnMuerte += HandleEnemigoMuerto;

            EnemigoSaltable scriptEnemigoSaltable = enemigoInstancia as EnemigoSaltable;
            if (scriptEnemigoSaltable != null)
            {
                
                scriptEnemigoSaltable.puntosDePatrulla = this.puntosDePatrullaEnemigo;
            }
            
        }
        else
        {
            Debug.LogError("El prefab del enemigo no tiene el script BaseEnemy!", this);
        }
    }

    private void HandleEnemigoMuerto(BaseEnemy enemigo)
    {
        Debug.Log("Enemigo en TimedPlatform murió. Activando movimiento.");
        ActivarMovimiento();

        if (enemigoInstancia != null)
        {
            enemigoInstancia.OnMuerte -= HandleEnemigoMuerto;
            enemigoInstancia = null;
        }
        
    }

    private void ActivarMovimiento()
    {
        puedeMoverse = true; 
    }

    private void OnDestroy()
    {
        if (enemigoInstancia != null)
        {
            enemigoInstancia.OnMuerte -= HandleEnemigoMuerto;
        }
    }
}