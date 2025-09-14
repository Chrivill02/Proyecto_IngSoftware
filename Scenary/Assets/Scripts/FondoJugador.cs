using UnityEngine;

public class FondoJugador : MonoBehaviour
{
    public float opacidad = 1;
    public Transform jugador;

    void LateUpdate()
    {
        transform.position = new Vector3(jugador.position.x, jugador.position.y + 2, transform.position.z);
    }
}
