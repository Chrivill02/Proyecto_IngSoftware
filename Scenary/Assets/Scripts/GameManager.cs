using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, PlayerObserver, MenuInterfaceInputObserver
{

    public bool gameOver = false;
    public bool tieneLlave = false;
    // -----------------------------

    public event Action OnGameStart;
    public event Action OnGameOver;

    void Start()
    {


        Player player = FindFirstObjectByType<Player>();


        if (player != null)
        {
            player.OnPlayerDeath += OnPlayerDeath;
        }
        else
        {
            Debug.LogWarning("GameManager: No se encontr� al Jugador. (Normal si es el men� principal)");
        }


        MenuInterfaceInputManager menuInputManager = FindFirstObjectByType<MenuInterfaceInputManager>();

        if (menuInputManager != null)
        {
            menuInputManager.OnContinueKeyPressed += OnContinueKeyPressed;
        }

        OnGameStart?.Invoke();
    }


    void Update()
    {

        if (gameOver)
        {
            OnGameOver?.Invoke();
        }

    }

    public void OnPlayerDeath()
    {
        gameOver = true;
    }

    public void OnContinueKeyPressed()
    {
        if (gameOver)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}