using System;
using UnityEngine;
using Zenject;

public class DesktopInput : IInput, ITickable, IInitializable, IDisposable, IDesktopInput
{
    private const int AttackButton = 0;

    private readonly InputSystem _inputSystem;
    private readonly PlayerRotator _rotator;

    private Vector3 _smoothDirection;
    private Vector3 _smoothVelocity;

    public event Action Attacked;
    public event Action<int> SwitchBulletButtonClicked;

    public DesktopInput(IPlayer player)
    {
        _rotator = new PlayerRotator(player);
        _inputSystem = new InputSystem();
    }

    public void Initialize()
    {
        _inputSystem.Enable();

        _inputSystem.Player.SwitchToBullet0.performed += ctx => SwitchTo(0);
        _inputSystem.Player.SwitchToBullet1.performed += ctx => SwitchTo(1);
        _inputSystem.Player.SwitchToBullet2.performed += ctx => SwitchTo(2);
        _inputSystem.Player.SwitchToBullet3.performed += ctx => SwitchTo(3);
        _inputSystem.Player.SwitchToBullet4.performed += ctx => SwitchTo(4);
    }

    public void Tick()
    {
        Attack();
        Rotate();
    }

    public void Dispose()
    {
        _inputSystem.Player.SwitchToBullet0.performed -= ctx => SwitchTo(0);
        _inputSystem.Player.SwitchToBullet1.performed -= ctx => SwitchTo(1);
        _inputSystem.Player.SwitchToBullet2.performed -= ctx => SwitchTo(2);
        _inputSystem.Player.SwitchToBullet3.performed -= ctx => SwitchTo(3);
        _inputSystem.Player.SwitchToBullet4.performed -= ctx => SwitchTo(4);

        _inputSystem.Disable();
    }

    private void Attack()
    {
        if (Input.GetMouseButton(AttackButton))
            Attacked?.Invoke();
    }

    private void Rotate() => _rotator.Rotate(Input.mousePosition);

    private void SwitchTo(int index) => SwitchBulletButtonClicked?.Invoke(index);
}