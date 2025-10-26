using System; 
using UnityEngine;
using UnityEngine.SceneManagement; 
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{ 
    [SerializeField] private GameObject menuPrincipal; 
    [SerializeField] private GameObject menuGameOver;
    [SerializeField] private SO_Inventario inventario; 

    public bool gameOver = false;
    public bool start = false;

    
    public event Action OnGameOver;
    public event Action OnGameStart;

    // Referencia al jugador 
    private Jugador jugador;

    void Start()
    {
        
        jugador = FindObjectOfType<Jugador>(); 
        if (jugador != null)
        {
            jugador.OnMuerteJugador += HandlePlayerDeath; 
        }
        else { Debug.LogError("GameManager no encontró al Jugador!"); }

       
        if (inventario != null) inventario.Reset();

       
        if (menuPrincipal != null) menuPrincipal.SetActive(true);
        if (menuGameOver != null) menuGameOver.SetActive(false);
        Time.timeScale = 0; 
    }

    void Update()
    {
        if (!start && !gameOver)
        {
            if (Input.GetKeyDown(KeyCode.X))
            { 
                StartGame();
            }
        }

        if (gameOver)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                RestartGame();
            }
        }
        
    }

    void StartGame()
    {
        start = true;
        gameOver = false;
        Time.timeScale = 1; // Reanudar juego
        if (menuPrincipal != null) menuPrincipal.SetActive(false);
        if (menuGameOver != null) menuGameOver.SetActive(false);
        OnGameStart?.Invoke(); 
        Debug.Log("Game Started");
    }

    
    public void HandlePlayerDeath()
    { // Cambiado a public por si acaso, pero idealmente es el handler del evento
        if (!gameOver)
        { // Evitar múltiples llamadas
            Debug.Log("Player Died - Game Over");
            gameOver = true;
            start = false; 
            Time.timeScale = 0; 
            if (menuGameOver != null) menuGameOver.SetActive(true);
            OnGameOver?.Invoke(); 
        }
    }

    void RestartGame()
    {
        Time.timeScale = 1; 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

   
    private void OnDestroy()
    {
        if (jugador != null)
        {
            jugador.OnMuerteJugador -= HandlePlayerDeath;
        }
    }
}