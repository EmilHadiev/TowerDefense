using UnityEngine;

public class DesktopMoveHandler : IMoveHandler
{
    private readonly InputSystem _inputSystem;

    public DesktopMoveHandler(IPlayerInputSystem playerInputSystem)
    {
        _inputSystem = new InputSystem();
        _inputSystem = playerInputSystem.InputSystem;
    }

    public Vector3 GetMoveDirection() => _inputSystem.Player.Move.ReadValue<Vector3>();
}