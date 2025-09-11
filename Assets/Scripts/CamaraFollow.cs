using UnityEngine;

public class CamaraFollow : MonoBehaviour
{
    public Transform jugador;   // arrastra aqu� tu personaje
    public Vector3 offset;      // para ajustar la posici�n de la c�mara respecto al jugador

    void LateUpdate()
    {
        if (jugador != null)
        {
            // La c�mara sigue siempre al jugador
            transform.position = new Vector3(
                jugador.position.x + offset.x,
                jugador.position.y + offset.y,
                transform.position.z
            );
        }
    }
}
