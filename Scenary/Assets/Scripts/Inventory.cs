using System;
using System.Collections.Generic;
using UnityEngine;




public class Inventory 
{
  private List<ItemData> items = new List<ItemData>();
  public event Action OnInventoryChanged;

  void Start()
  {
    
  }

  public void AddItem(ItemData item)
  {
    items.Add(item);
    OnInventoryChanged?.Invoke();
  }

  public void RemoveItem(ItemData item)
  {
    items.Remove(item);
    OnInventoryChanged?.Invoke();
  }

}


