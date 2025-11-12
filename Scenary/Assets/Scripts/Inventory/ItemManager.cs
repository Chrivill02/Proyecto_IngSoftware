using UnityEngine;



public class ItemManager : InteractableItemObserver
{
  public Inventory inventory;
  public void OnItemPickedUp(ItemData item)
  {
    inventory.AddItem(item);
  }

  public void OnDoorUnlocked(ItemData item)
  {
    inventory.RemoveItem(item);
  }

}