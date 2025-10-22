using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class EnemyChaseTrigger : MonoBehaviour
{
    public float speed = 3f;
    public string playerTag = "Player";

    Rigidbody2D rb;
    Transform player;
    bool chasing = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
    }

    void FixedUpdate()
    {
        if (chasing && player != null)
        {
            float dirX = Mathf.Sign(player.position.x - transform.position.x);
            float distX = Mathf.Abs(player.position.x - transform.position.x);
            float moveX = distX > 0.05f ? dirX * speed : 0f;

            rb.linearVelocity = new Vector2(moveX, rb.linearVelocity.y);

            FlipSprite(dirX);
        }
        else
        {
            rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(playerTag)) return; 
        player = other.transform;
        chasing = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            chasing = false;
            player = null;
        }
    }

    void FlipSprite(float dirX)
    {
        if (dirX == 0) return;
        Vector3 s = transform.localScale;
        s.x = Mathf.Abs(s.x) * (dirX < 0 ? -1 : 1);
        transform.localScale = s;
    }
}
