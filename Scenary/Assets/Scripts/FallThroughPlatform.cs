using System;
using System.Collections;
using UnityEngine;

public class FallTroughPlatform : MonoBehaviour
{
    private Collider2D platformCollider;
    private bool isPlayerOnPlatform = false;

    private void Start()
    {
        platformCollider = GetComponent<Collider2D>();

        
    }

    private void Update()
    {
        if (isPlayerOnPlatform && Input.GetAxisRaw("Vertical") < 0)
        {
            platformCollider.enabled = false;
            StartCoroutine(ReenableCollider());
        }
    }

    private IEnumerator ReenableCollider()
    {
        yield return new WaitForSeconds(0.5f);
        platformCollider.enabled = true;
    }

    private void SetPlayerOnPlatform(Collision2D other, bool value)
    {
        var player = other.gameObject.GetComponent<Player>();
        Console.WriteLine("Checking collision with: " + other.gameObject.name);
        if (player != null)
        {
            Console.WriteLine("Player on platform state changed to: " + value);
            isPlayerOnPlatform = value;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        SetPlayerOnPlatform(collision, true);
    }
  
    private void OnCollisionExit2D(Collision2D collision)
    {
        SetPlayerOnPlatform(collision, false);
    }

}
