using UnityEngine;

public class GotaV : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private Vector3 inicial;
    private Quaternion rotacionInicial;
    void Start()
    {
        inicial = transform.position;
        rotacionInicial = transform.rotation;
    }

    // Update is called once per frame

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Suelo") || collision.gameObject.tag == "Obstaculo" || collision.gameObject.tag == "Player" || collision.gameObject.tag == "Enemy")
        {
            transform.position = inicial;
            transform.rotation = rotacionInicial;
        } 
    }
    void Update()
    {
        
       
    }
}
