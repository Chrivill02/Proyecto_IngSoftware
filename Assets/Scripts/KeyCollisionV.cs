using UnityEngine;

public class KeyCollision : MonoBehaviour
{
    public bool stolen = false;


  void OnCollisionEnter2D(Collision2D collision)
  {
    if (collision.gameObject.CompareTag("Player"))
    {
            stolen = true;
    }
  }
}
    