using UnityEngine;
using UnityEngine.SceneManagement; 

public class MainMenuManager : MonoBehaviour
{
    public void CargarNivel(string nombreDelNivel)
    {
        
        SceneManager.LoadScene(nombreDelNivel);
    }

    
    public void SalirDelJuego()
    {
        
        Debug.Log("¡Saliendo del juego!"); 
        Application.Quit();
    }
}