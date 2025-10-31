using UnityEngine;

public class Llave : MonoBehaviour
{

    private NewMonoBehaviourScript gameManager;

  

    void Start()
    {
       
        gameManager = FindObjectOfType<NewMonoBehaviourScript>();

        if (gameManager == null)
        {
            Debug.LogError("¡La Llave no pudo encontrar el Game Manager (NewMonoBehaviourScript) en la escena!");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (gameManager != null)
            {
                // 1. Avisa al Game Manager que el jugador tiene la llave
                gameManager.tieneLlave = true;
                Debug.Log("¡Llave recogida!");

              
                Destroy(gameObject);
            }
        }
    }
}