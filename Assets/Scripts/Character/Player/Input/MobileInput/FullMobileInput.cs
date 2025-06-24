using UnityEngine;

/// <summary>
/// if you also need to use map movement. 
/// The player must also have the PlayerMover and PlayerAnimator components
/// </summary>
class FullMobileInput : BaseMobileInput, IMoveHandler
{
    private readonly Joystick _moveJoystick;

    public FullMobileInput(IJoystickFactory joystickFactory, IPlayerRotator rotator)
        : base(joystickFactory, rotator, AssetProvider.MobileInputAttackPath)
    {
        _moveJoystick = CreateJoystick(joystickFactory, AssetProvider.MobileInputMoverPath);
    }

    public override void Tick()
    {
        if (IsWork == false) 
            return;

        HandleTouch();
    }

    public Vector3 GetMoveDirection()
    {
        return new Vector3(_moveJoystick.Horizontal, 0, _moveJoystick.Vertical);
    }

    public override void Continue()
    {
        base.Continue();
        _moveJoystick.gameObject.SetActive(true);
    }

    public override void Stop()
    {
        base.Stop();
        _moveJoystick.gameObject.SetActive(false);
    }
}