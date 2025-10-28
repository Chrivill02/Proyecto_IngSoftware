public interface PlayerMovementInputObserver : PlayerInputObserver
{
  public void OnJumpKeyPressed();
  public void OnMove(float direction);

}