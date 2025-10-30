using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public GameObject menuPrincipal;
    public GameObject menuGameOver;

    public float velocidad = 2;
    public GameObject col;
    public GameObject piedra1;
    public GameObject piedra2;
    public Renderer fondo;
    public bool gameOver = false;
    public bool start = false;  

    public List<GameObject> cols = new List<GameObject>();
    public List<GameObject> obstaculos = new List<GameObject>();

    void Start()
    {
        // Crear Mapa
        for (int i = 0; i < 70; i++)
        {
            cols.Add(Instantiate(col, new Vector2(-10 + i, -3), Quaternion.identity));
        }

        // Asegurarse de que el menú de Game Over esté oculto al inicio
        menuGameOver.SetActive(false);
        menuPrincipal.SetActive(true);
    }

    void Update()
    {
        // Comenzar juego con X
        if (!start)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                start = true;
                menuPrincipal.SetActive(false);
            }
        }

        // Si el juego terminó
        if (start && gameOver)
        {
            menuGameOver.SetActive(true);
            Time.timeScale = 0f; // Pausa el juego
            if (Input.GetKeyDown(KeyCode.X))
            {
                Time.timeScale = 1f; // Reiniciar tiempo antes de recargar escena
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            return; // Sale de Update para que no se mueva nada más
        }

        // Movimiento del fondo y objetos si el juego está activo
        if (start && !gameOver)
        {
            // Fondo
            fondo.material.mainTextureOffset += new Vector2(0.03f, 0) * Time.deltaTime;

            // Mapa
            for (int i = 0; i < cols.Count; i++)
            {
                if (cols[i].transform.position.x <= -10)
                    cols[i].transform.position = new Vector3(10, -3, 0);

                cols[i].transform.position += new Vector3(-1, 0, 0) * Time.deltaTime * velocidad;
            }

            // Obstáculos
            for (int i = 0; i < obstaculos.Count; i++)
            {
                if (obstaculos[i].transform.position.x <= -10)
                {
                    float randomObs = Random.Range(11f, 18f);
                    obstaculos[i].transform.position = new Vector3(randomObs, -2, 0);
                }
                obstaculos[i].transform.position += new Vector3(-1, 0, 0) * Time.deltaTime * velocidad;
            }
        }
    }

    // Detecta colisiones con enemigos
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            gameOver = true;
        }
    }
}
