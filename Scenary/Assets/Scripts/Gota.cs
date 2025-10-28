using UnityEngine;

public class Gota : MonoBehaviour, FallingEnemy
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private Vector3 initialPosition;
    private Quaternion rotacionInicial;
    void Start()
    {
        initialPosition = transform.position;
        rotacionInicial = transform.rotation;
    }

    // Update is called once per frame

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Suelo") ||
        collision.gameObject.tag == "Obstaculo" ||
        collision.gameObject.tag == "Playe-r" ||
        collision.gameObject.tag == "Enemy")
        {
            OnGroundDetected();
        }
    }
    
    public void OnGroundDetected()
    {
        transform.position = initialPosition;
        transform.rotation = rotacionInicial;
    }
}
