using UnityEngine;

public class PlataformaSemisolida : MonoBehaviour
{
    public Collider2D colPlataforma;  // Collider sólido
    private bool jugadorEncima = false;

    void Start()
    {
        if (colPlataforma == null)
            colPlataforma = GetComponent<Collider2D>();
    }

    // Este método será llamado desde el hijo Detector
    public void ActivarPlataforma(bool activar)
    {
        colPlataforma.enabled = activar;
        jugadorEncima = activar;
    }
}
