using UnityEngine;

public class Gota : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private Vector3 inicial;
    void Start()
    {
        inicial = transform.position;
    }

    // Update is called once per frame

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Suelo") || collision.gameObject.tag == "Obstaculo")
        {
            transform.position = inicial;
            
        } 
    }
    void Update()
    {
       
       
    }
}
