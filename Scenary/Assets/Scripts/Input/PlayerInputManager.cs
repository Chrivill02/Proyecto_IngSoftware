using System;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour, InputManager
{

  public event Action OnJumpKeyPressed;
  public event Action OnShootKeyPressed;
  public event Action<float> OnMoveKeyPressed;

  void Update()
  {
    if (Input.GetKeyDown(KeyCode.Space))
    {
      OnJumpKeyPressed?.Invoke();
    }

    if (Input.GetKeyDown(KeyCode.Z))
    {
      OnShootKeyPressed?.Invoke();
    }
    
    OnMoveKeyPressed?.Invoke(Input.GetAxis("Horizontal"));
  }

}