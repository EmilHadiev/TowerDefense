using UnityEngine;

public class DesktopMoveHandler : IMoveHandler
{
    private readonly InputSystem _inputSystem;

    public DesktopMoveHandler()
    {
        _inputSystem = new InputSystem();
        _inputSystem.Enable();
    }

    public Vector3 GetMoveDirection() => _inputSystem.Player.Move.ReadValue<Vector3>();
}