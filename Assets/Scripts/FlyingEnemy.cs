using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    // La velocidad ya no necesita ser pública aquí, el spawner la controlará
    private float speed;
    private int direction = 0;

    // Nuevo método para configurar dirección y velocidad al mismo tiempo
    public void Initialize(int dir, float newSpeed)
    {
        direction = dir;
        speed = newSpeed; // Asigna la velocidad que viene del spawner

        // Voltear el sprite si va a la izquierda
        if (direction < 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    void Update()
    {
        if (direction != 0)
        {
            transform.Translate(Vector2.right * direction * speed * Time.deltaTime);
        }

        // Destrucción fuera de pantalla
        if (!GetComponent<Renderer>().isVisible)
        {
            Destroy(gameObject, 5f);
        }
    }
}