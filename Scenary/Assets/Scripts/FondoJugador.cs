using UnityEngine;

public class FondoJugador : MonoBehaviour
{
    public Transform jugador;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void LateUpdate()
    {
        transform.position = new Vector3(jugador.position.x, jugador.position.y + 2, transform.position.z);
    }
}
