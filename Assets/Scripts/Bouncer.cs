using UnityEngine;

public class Bouncer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            Animator animator = collision.gameObject.GetComponent<Animator>();
            if (rb != null)
            {
                animator.SetBool("estaSaltando", true);
                rb.linearVelocity = new Vector2(rb.linearVelocity.y, 10f); // Ajusta el valor de 10f según la fuerza que desees
            }
        }
    }
}
