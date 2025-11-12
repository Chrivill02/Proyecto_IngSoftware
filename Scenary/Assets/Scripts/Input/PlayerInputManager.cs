using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour, InputManager
{

  public event Action OnJumpKeyPressed;
  public event Action OnShootKeyPressed;
  public event Action<float> OnMoveKeyPressed;

  public void OnMove(InputValue value)
  {
    Vector2 input = value.Get<Vector2>();
    float horizontal = input.x;
    OnMoveKeyPressed?.Invoke(horizontal);  
  }

  public void OnJump()
  {
    OnJumpKeyPressed?.Invoke();
  }

  public void OnFire()
  {
    OnShootKeyPressed?.Invoke();
  }

}