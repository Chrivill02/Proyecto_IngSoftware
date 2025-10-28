using System;
using UnityEngine;




public class KeyItem : MonoBehaviour, InteractableObject
{
  public ItemData keyData;
  public event Action OnKeyCollected;

  void Start()
  {
    keyData.itemIcon = GetComponent<SpriteRenderer>().sprite;
    keyData.itemName = gameObject.name;
  }

  void OnCollisionEnter2D(Collision2D collision)
  {
    if (collision.gameObject.CompareTag("Player"))
    {
      Interact();
    }
  }

  public void Interact()
  {
    OnKeyCollected?.Invoke();
    Destroy(gameObject);
  }
}
