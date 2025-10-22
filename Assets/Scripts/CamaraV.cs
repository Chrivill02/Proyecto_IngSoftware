using UnityEngine;

public class CamaraSeguirJugador : MonoBehaviour
{
    public Transform jugador;      // Referencia al jugador
    public float suavizado = 0.125f;
    public Vector3 offset;         // Por si quieres ajustar la posición de la cámara

    [Header("Límites del mapa")]
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    void LateUpdate()
    {
        if (jugador == null) return;

        // Posición deseada con offset
        Vector3 posicionDeseada = jugador.position + offset;

        // Limitamos la posición dentro de los bordes
        float posX = Mathf.Clamp(posicionDeseada.x, minX, maxX);
        float posY = Mathf.Clamp(posicionDeseada.y, minY, maxY);

        Vector3 posicionLimitada = new Vector3(posX, posY, transform.position.z);

        // Movimiento suavizado
        transform.position = Vector3.Lerp(transform.position, posicionLimitada, suavizado);
    }
}
