using System.Collections.Generic;
using UnityEngine.InputSystem;

public class BulletSwitcherHandler
{
    private readonly InputSystem _inputSystem;

    public readonly IReadOnlyCollection<InputAction> Actions;

    public BulletSwitcherHandler(InputSystem inputSystem)
    {
        _inputSystem = inputSystem;
        Actions = GetInputActions();
    }

    private InputAction[] GetInputActions()
    {
        return new InputAction[]
        {
            _inputSystem.Player.SwitchToBullet0,
            _inputSystem.Player.SwitchToBullet1,
            _inputSystem.Player.SwitchToBullet2,
            _inputSystem.Player.SwitchToBullet3,
            _inputSystem.Player.SwitchToBullet4,
            _inputSystem.Player.SwitchToBullet5,
            _inputSystem.Player.SwitchToBullet6,
            _inputSystem.Player.SwitchToBullet7,
            _inputSystem.Player.SwitchToBullet8,
            _inputSystem.Player.SwitchToBullet9,
        };
    }
}