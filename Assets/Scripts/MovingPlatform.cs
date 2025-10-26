using UnityEngine;
public abstract class MovingPlatform : MonoBehaviour
{
    [SerializeField] protected Transform pointA; // Usar Transforms es más flexible
    [SerializeField] protected Transform pointB;
    [SerializeField] protected float speed = 2f;
    [SerializeField] protected float waitTime = 1f;

    protected Rigidbody2D rb;
    protected Vector3 targetPosition;
    protected bool waiting = false;
    protected float waitCounter;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true; // Plataformas suelen ser Kinematic
        transform.position = pointA.position;
        targetPosition = pointB.position;
        waitCounter = waitTime;
    }

    protected virtual void FixedUpdate()
    { // Usar FixedUpdate para movimiento físico
        if (waiting)
        {
            waitCounter -= Time.fixedDeltaTime;
            if (waitCounter <= 0)
            {
                waiting = false;
                waitCounter = waitTime;
                // Swap target
                targetPosition = (targetPosition == pointA.position) ? pointB.position : pointA.position;
            }
        }
        else
        {
            Vector2 newPos = Vector2.MoveTowards(rb.position, targetPosition, speed * Time.fixedDeltaTime);
            rb.MovePosition(newPos);

            if (Vector2.Distance(rb.position, targetPosition) < 0.1f)
            {
                waiting = true;
            }
        }
    }
    // Gizmos para visualizar en el editor
    protected virtual void OnDrawGizmos()
    {
        if (pointA != null && pointB != null)
        {
            
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(pointA.position, pointB.position);

            
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(pointA.position, 0.3f); // Esfera roja en pointA

            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(pointB.position, 0.3f); // Esfera verde en pointB
        }
    }
}