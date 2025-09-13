using UnityEngine;

public class Camara : MonoBehaviour
{
    public Transform jugador;
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - jugador.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = jugador.position + offset;
    }
}
