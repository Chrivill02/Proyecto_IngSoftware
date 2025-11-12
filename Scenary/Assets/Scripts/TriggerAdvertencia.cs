using UnityEngine;

public class TriggerAdvertencia : MonoBehaviour
{
    [TextArea(3, 5)]
    public string mensajeParaMostrar;

    // Ya no usaremos el tag del panel, ahora usaremos el del Canvas
    private string tagDelCanvas = "UICanvas"; // ¡Asegúrate que este tag exista y esté en tu Canvas!

    private bool yaSeMostro = false;
    private PanelAdvertencia panelScript;

    void Start()
    {
        // 1. Buscamos el objeto Canvas usando su Tag
        GameObject canvasObjeto = GameObject.FindWithTag(tagDelCanvas);

        if (canvasObjeto != null)
        {
            // 2. Le pedimos al Canvas que busque EN TODOS SUS HIJOS
            // (incluyendo los inactivos) el script "PanelAdvertencia"
            // ¡El 'true' es la magia que busca en hijos inactivos!
            panelScript = canvasObjeto.GetComponentInChildren<PanelAdvertencia>(true);
        }

        // 3. Verificación de errores mejorada
        if (panelScript == null)
        {
            Debug.LogError("¡Error! No se pudo encontrar 'PanelAdvertencia.cs' como hijo del Canvas con Tag: " + tagDelCanvas);
        }
    }

    // El resto del script (OnTriggerEnter2D) se queda exactamente igual
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !yaSeMostro && panelScript != null)
        {
            panelScript.MostrarAdvertencia(mensajeParaMostrar);
            yaSeMostro = true;
        }
    }
}