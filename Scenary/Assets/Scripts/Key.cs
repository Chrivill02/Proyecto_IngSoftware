using UnityEngine;

public class Llave : MonoBehaviour
{
    // 1. Cambiamos 'NewMonoBehaviourScript' por 'GameManager'
    private GameManager gameManager;
    public bool stolen = false;

    void Start()
    {
        // 2. Buscamos el tipo 'GameManager'
        gameManager = FindObjectOfType<GameManager>();

        if (gameManager == null)
        {
            // 3. Actualizamos el mensaje de error
            Debug.LogError("�La Llave no pudo encontrar el GameManager en la escena!");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (gameManager != null)
            {
                // 4. Esta l�nea ahora funcionar� porque
                //    a�adiremos 'tieneLlave' al GameManager
                gameManager.tieneLlave = true;
                Debug.Log("�Llave recogida!");
                stolen = true;


                Destroy(gameObject);
            }
        }
    }
}
