using UnityEngine;
using UnityEngine.AdaptivePerformance.VisualScripting;

public class SinkingPlatform : MonoBehaviour
{
    public float speed;
    Vector2 originPos;

    public bool triggered;

    // Start is called once before the first execution of Update after the MonoBehaviour is created


    void Start()
    {
        originPos = transform.position;
        triggered = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (triggered)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(originPos.x, originPos.y - 3f), speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, originPos, speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            triggered = true;
            collision.transform.parent = transform;
        }
    }
    
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            triggered = false;
            collision.transform.parent = null;
        }
    }
}
