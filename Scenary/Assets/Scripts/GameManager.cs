using System;
using UnityEngine;

public class GameManager : MonoBehaviour, IPlayerObserver
{
    public event Action OnGameOver;
    public event Action OnGameStart;

    public bool gameOver = false;
    public bool start = false;  

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        Jugador playerScript = FindAnyObjectByType<Jugador>(); 
    
        if (playerScript != null)
        {
            playerScript.OnMuerteJugador += OnPlayerDeath;

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!start)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                start = true;
            }
        }

        if (start && gameOver)
        {
            OnGameOver?.Invoke();
        }

        if (start && !gameOver)
        {
            OnGameStart?.Invoke();

            /*
            //Mapa
            for (int i = 0; i < cols.Count; i++)
            {
                if (cols[i].transform.position.x <= -10)
                {
                    cols[i].transform.position = new Vector3(10, -3, 0);
                }
                cols[i].transform.position = cols[i].transform.position + new Vector3(-1, 0, 0) * Time.deltaTime * velocidad;
            }

            //Obstaculos
            for (int i = 0; i < obstaculos.Count; i++)
            {
                if (obstaculos[i].transform.position.x <= -10)
                {
                    float randomObs = Random.Range(11, 18);
                    obstaculos[i].transform.position = new Vector3(randomObs, -2, 0);
                }
                obstaculos[i].transform.position = obstaculos[i].transform.position + new Vector3(-1, 0, 0) * Time.deltaTime * velocidad;
            }
            */
        }
    }
    public void OnPlayerDeath()
    {
        if (!gameOver)
        {
            gameOver = true;
            
        }
    }
}