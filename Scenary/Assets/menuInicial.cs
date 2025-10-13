using UnityEngine;
using UnityEngine.SceneManagement;

public class menuInicial : MonoBehaviour
{

public void Jugar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

 public void Salir()
    {
        Debug.Log("Salir...");
        Application.Quit();
    }
}
