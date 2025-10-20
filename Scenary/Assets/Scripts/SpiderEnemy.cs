using UnityEngine;

public class SpiderEnemy : MonoBehaviour
{
    public float speed = 2f;
    public Transform[] patrolPoints;
    private int currentPoint = 0;

    void Update()
    {
        if (patrolPoints.Length == 0)
            return;

        Transform target = patrolPoints[currentPoint];
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, target.position) < 0.1f)
        {
            currentPoint = (currentPoint + 1) % patrolPoints.Length;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Lógica para dañar al jugador
            Debug.Log("¡La araña ha tocado al jugador!");
        }
    }
}
