using UnityEngine;

public class MobileMoveHandler : IMoveHandler
{
    private readonly IJoystickFactory _joystickFactory;
    private readonly Joystick _joystick;


    public Vector3 GetMoveDirection()
    {
        return Vector3.zero;
    }
}