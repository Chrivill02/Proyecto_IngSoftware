using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2f;
    public Transform[] puntos; // puntos de patrulla
    private int currentPoint = 0;

    void Update()
    {
        if (puntos.Length == 0) return;

        // Moverse hacia el siguiente punto
        transform.position = Vector2.MoveTowards(transform.position, puntos[currentPoint].position, speed * Time.deltaTime);

        // Cambiar de punto al llegar
        if (Vector2.Distance(transform.position, puntos[currentPoint].position) < 0.1f)
        {
            currentPoint++;
            if (currentPoint >= puntos.Length)
                currentPoint = 0;
        }
    }
}
