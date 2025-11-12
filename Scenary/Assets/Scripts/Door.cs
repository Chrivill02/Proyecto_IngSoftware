using UnityEngine;
using UnityEngine.SceneManagement;

public class Puerta : MonoBehaviour
{

    // 1. Cambiamos 'NewMonoBehaviourScript' por 'GameManager'
    public GameManager gameManager;


    public string nombreSiguienteNivel;

    // 2. (Opcional) Es mejor asignar el GameManager desde el Start
    //    o desde el Inspector de Unity.
    void Start()
    {
        // Si no has arrastrado el GameManager al Inspector,
        // lo buscamos al iniciar.
        if (gameManager == null)
        {
            gameManager = FindObjectOfType<GameManager>();
        }

        if (gameManager == null)
        {
            Debug.LogError("¡La Puerta no pudo encontrar el GameManager en la escena!");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {

            // 3. Comprobamos si la referencia sigue siendo nula
            if (gameManager == null)
            {
                Debug.LogError("¡La Puerta no tiene referencia al Game Manager!");
                return; // Salimos para evitar un error
            }


            // 4. Esta comprobación ahora funciona
            if (gameManager.tieneLlave)
            {
                Debug.Log("¡Puerta abierta! Cargando siguiente nivel...");


                SceneManager.LoadScene(nombreSiguienteNivel);
            }
            else
            {
                Debug.Log("¡Te falta la llave!");

            }
        }
    }
}