using System;
using UnityEngine;

public class MenuInterfaceInputManager : MonoBehaviour
{
  public event Action OnContinueKeyPressed;
  void Update()
  {
    if (Input.GetKeyDown(KeyCode.X))
    {
      OnContinueKeyPressed?.Invoke();
    }
  }
}