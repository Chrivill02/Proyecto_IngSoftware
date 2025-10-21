using UnityEngine;
using UnityEngine.SceneManagement; 

public class Puerta : MonoBehaviour
{
    
    public NewMonoBehaviourScript gameManager;

    
    public string nombreSiguienteNivel;

    private void OnTriggerEnter2D(Collider2D other)
    {
       
        if (other.CompareTag("Player"))
        {
            
            if (gameManager == null)
            {
                Debug.LogError("¡La Puerta no tiene referencia al Game Manager!");
                
                gameManager = FindObjectOfType<NewMonoBehaviourScript>();
                if (gameManager == null) return;
            }

           
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