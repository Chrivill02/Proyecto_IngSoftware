using UnityEngine;

public class StartGameInput : MonoBehaviour
{
    private GameManager gameManager;

    void Start()
    {
        // Buscar el GameManager en la escena
        gameManager = FindFirstObjectByType<GameManager>();
    }

    void Update()
    {
        // Detectar la tecla X para iniciar
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (gameManager != null)
            {
                // Simular que se presionó la tecla continuar
                gameManager.OnContinueKeyPressed();
                Debug.Log("Se presionó X — Juego iniciado manualmente");
            }
            else
            {
                Debug.LogWarning("No se encontró el GameManager en la escena.");
            }
        }
    }
}
