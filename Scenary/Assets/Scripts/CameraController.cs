using UnityEngine;


public class CameraController : MonoBehaviour
{
    public Transform objetivo;
    public float speed = 0.025f;
    public Vector3 desplazamiento;

    private void LateUpdate()
    {
        if (objetivo != null)
        {
            Vector3 posicionDeseada = objetivo.position + desplazamiento;

            Vector3 posicionSuavizada = Vector3.Lerp(transform.position, posicionDeseada, speed);

            transform.position = posicionSuavizada;

        }
    }
}
