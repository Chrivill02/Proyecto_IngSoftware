using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, PlayerObserver, MenuInterfaceInputObserver
{

    public bool gameOver = false;
    public bool start = false;

    // --- ¡AQUÍ ESTÁ EL CAMBIO! ---
    // Añadimos la variable pública que la llave necesita.
    // Por defecto será 'false' al iniciar.
    public bool tieneLlave = false;
    // -----------------------------

    public event Action OnGameStart;
    public event Action OnGameOver;

    void Start()
    {
       
        // pero aquí buscabas 'Player'. Lo corrijo.
        Player player = FindFirstObjectByType<Player>();

        // Añadimos una comprobación por si el GameManager
        // está en una escena de menú donde no hay jugador.
        if (player != null)
        {
            player.OnPlayerDeath += OnPlayerDeath;
        }
        else
        {
            Debug.LogWarning("GameManager: No se encontró al Jugador. (Normal si es el menú principal)");
        }


        MenuInterfaceInputManager menuInputManager = FindFirstObjectByType<MenuInterfaceInputManager>();

        if (menuInputManager != null)
        {
            menuInputManager.OnContinueKeyPressed += OnContinueKeyPressed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (start && gameOver)
        {
            OnGameOver?.Invoke();
        }

        if (start && !gameOver)
        {
            OnGameStart?.Invoke();
        }
    }

    public void OnPlayerDeath()
    {
        gameOver = true;
    }

    public void OnContinueKeyPressed()
    {
        if (!start)
        {
            start = true;
        }
        else if (start && gameOver)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}