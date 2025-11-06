using System;
using UnityEngine;

public class MenuInterfaceInputManager : MonoBehaviour
{
  public event Action OnContinueKeyPressed;

  void OnContinue()
  {
    OnContinueKeyPressed?.Invoke();
  }
}