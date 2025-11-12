using UnityEngine;
using TMPro; // ¡Importante! Añade esto para usar TextMeshPro

public class PanelAdvertencia : MonoBehaviour
{
    // Arrastra aquí tu objeto de Texto (el que está DENTRO del panel)
    public TextMeshProUGUI textoDelPanel;

    // Esta función la llamarán los triggers
    public void MostrarAdvertencia(string mensaje)
    {
        // 1. Asignamos el mensaje que nos pasaron
        if (textoDelPanel != null)
        {
            textoDelPanel.text = mensaje;
        }

        // 2. Nos activamos
        gameObject.SetActive(true);

        // 3. Pausamos el juego
        Time.timeScale = 0f;
    }

    // Esto se mantiene igual para detectar el clic
    void Update()
    {
        // Solo detecta el clic si el panel está activo
        if (gameObject.activeInHierarchy && Input.GetMouseButtonDown(0))
        {
            OcultarPanel();
        }
    }

    void OcultarPanel()
    {
        // Nos desactivamos
        gameObject.SetActive(false);

        // Reanudamos el juego
        Time.timeScale = 1f;
    }
}