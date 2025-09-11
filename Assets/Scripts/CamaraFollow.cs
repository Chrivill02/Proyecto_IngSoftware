using UnityEngine;

public class CamaraFollow : MonoBehaviour
{
    public Transform jugador;   // arrastra aquí tu personaje
    public Vector3 offset;      // para ajustar la posición de la cámara respecto al jugador

    void LateUpdate()
    {
        if (jugador != null)
        {
            // La cámara sigue siempre al jugador
            transform.position = new Vector3(
                jugador.position.x + offset.x,
                jugador.position.y + offset.y,
                transform.position.z
            );
        }
    }
}
