using UnityEngine;

public class KeyCollision : MonoBehaviour
{
  public bool stolen = false;
  public GameManager gameManager;

  void Start()
  {
      gameManager = FindObjectOfType<GameManager>();

  }


  void OnCollisionEnter2D(Collision2D collision)
  {
    if (collision.gameObject.CompareTag("Player"))
    {
      stolen = true;
      gameManager.tieneLlave = true;
            
    }
  }
}
    