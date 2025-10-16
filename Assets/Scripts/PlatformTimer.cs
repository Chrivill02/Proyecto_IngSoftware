using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))] // Asegura que el objeto tenga un Rigidbody2D.
public class PlatformTimer : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveDistance = 0.5f;
    public float moveSpeed = 1.0f;
    public float waitTime = 5.0f;
    public float verticalLimit = 10.0f;
    public float returnDelay = 2.0f;

    private Vector3 initialPosition;
    private Coroutine platformCoroutine;
    private Rigidbody2D rb; // Referencia al Rigidbody2D.

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true; // Asegurarse de que sea Kinematic.
        initialPosition = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // El jugador ahora se mueve con la plataforma.
            collision.transform.SetParent(transform);

            if (platformCoroutine != null) StopCoroutine(platformCoroutine);
            platformCoroutine = StartCoroutine(MoveUpRoutine());
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // El jugador ya no es hijo de la plataforma.
            collision.transform.SetParent(null);

            if (platformCoroutine != null) StopCoroutine(platformCoroutine);
            platformCoroutine = StartCoroutine(ReturnAfterDelayRoutine());
        }
    }

    // Rutina principal para el movimiento ascendente.
    private IEnumerator MoveUpRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);

            Vector3 nextPosition = rb.position + new Vector2(0, moveDistance);

            if (nextPosition.y > verticalLimit)
            {
                yield return StartCoroutine(MoveToPosition(new Vector2(rb.position.x, verticalLimit)));
                yield break;
            }

            yield return StartCoroutine(MoveToPosition(nextPosition));
        }
    }

    // Rutina que espera antes de regresar.
    private IEnumerator ReturnAfterDelayRoutine()
    {
        yield return new WaitForSeconds(returnDelay);
        yield return StartCoroutine(MoveToPosition(initialPosition));
    }

    // Mueve suavemente la plataforma a una posición (usando físicas).
    private IEnumerator MoveToPosition(Vector2 target)
    {
        while (Vector2.Distance(rb.position, target) > 0.01f)
        {
            // Calcula la nueva posición y la mueve usando el Rigidbody.
            Vector2 newPosition = Vector2.MoveTowards(rb.position, target, moveSpeed * Time.fixedDeltaTime);
            rb.MovePosition(newPosition);
            yield return new WaitForFixedUpdate(); // Sincroniza con el ciclo de físicas.
        }
        rb.MovePosition(target); // Asegura la posición final exacta.
    }
}