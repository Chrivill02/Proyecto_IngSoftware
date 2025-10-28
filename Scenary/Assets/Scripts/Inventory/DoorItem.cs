using System;
using UnityEngine;




public class DoorItem : MonoBehaviour, InteractableObject
{
  public event Action OnDoorUnlocked;

  void OnCollisionEnter2D(Collision2D collision)
  {
    if (collision.gameObject.CompareTag("Player"))
    {
      Interact();
    }
  }

  public void Interact()
  {
    OnDoorUnlocked?.Invoke();
  }

}
