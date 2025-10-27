using UnityEngine;

public class ZonaPeligrosa : MonoBehaviour
{
    public Transform player;
    public float detectionRadius;
    public float speed;


    private Rigidbody2D Rigidbody2D;
    private Vector2 movement;
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlay = Vector2.Distance(transform.position, player.position);
        if (distanceToPlay < detectionRadius)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            movement = new Vector2(direction.x, 0);
        }
        else
        {
            movement = Vector2.zero;
        }

        Rigidbody2D.MovePosition(Rigidbody2D.position + movement * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector2 direccionDanio = new Vector2(transform.position.x, 0);
        }
    }
}
