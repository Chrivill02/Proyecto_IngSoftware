using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuInterfaceInputManager : MonoBehaviour, InputManager
{
  public event Action OnContinueKeyPressed;

  public void OnContinue()
  {
    Debug.Log("entroo");
    OnContinueKeyPressed?.Invoke();
  }
}