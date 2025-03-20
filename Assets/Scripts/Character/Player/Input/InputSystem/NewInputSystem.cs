using System;
using Zenject;

class NewInputSystem : IInputSystem, IInitializable, IDisposable
{
    private readonly InputSystem _inputSystem;

    public event Action<int> SwitchBulletButtonClicked;

    public NewInputSystem()
    {
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

    public void Dispose()
    {
        _inputSystem.Player.SwitchToBullet0.performed -= ctx => SwitchTo(0);
        _inputSystem.Player.SwitchToBullet1.performed -= ctx => SwitchTo(1);
        _inputSystem.Player.SwitchToBullet2.performed -= ctx => SwitchTo(2);
        _inputSystem.Player.SwitchToBullet3.performed -= ctx => SwitchTo(3);
        _inputSystem.Player.SwitchToBullet4.performed -= ctx => SwitchTo(4);

        _inputSystem.Disable();
    }

    private void SwitchTo(int index) => SwitchBulletButtonClicked?.Invoke(index);
}
