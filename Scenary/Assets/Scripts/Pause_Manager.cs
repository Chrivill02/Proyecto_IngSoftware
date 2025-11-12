using UnityEngine;
using UnityEngine.SceneManagement; // <-- ¡Necesario para cambiar de escena!

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenuPanel; // Arrastra tu panel aquí

    // Esta variable nos dirá si el juego ya está pausado
    private bool isPaused = false;

    // Esta función la llamará el botón de las 3 barritas
    public void PausarJuego()
    {
        isPaused = true;
        pauseMenuPanel.SetActive(true); // Muestra el panel
        Time.timeScale = 0f; // ¡Detiene el tiempo del juego!
    }

    // Esta función la llamará el botón "Reanudar"
    public void ReanudarJuego()
    {
        isPaused = false;
        pauseMenuPanel.SetActive(false); // Oculta el panel
        Time.timeScale = 1f; // Reanuda el tiempo del juego
    }

    // Esta función la llamará el botón "Salir al Menú"
    public void VolverAlMenu()
    {
        // ¡MUY IMPORTANTE! Siempre resetea el tiempo antes de salir.
        Time.timeScale = 1f;
        SceneManager.LoadScene("InitialMenu"); // Cambia "MainMenu" por el nombre exacto de tu escena de menú
    }

   
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ReanudarJuego();
            }
            else
            {
                PausarJuego();
            }
        }
    }
}