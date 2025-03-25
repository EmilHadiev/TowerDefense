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

        _inputSystem.Player.SwitchToBullet0.performed += ctx => SwitchBulletTo(0);
        _inputSystem.Player.SwitchToBullet1.performed += ctx => SwitchBulletTo(1);
        _inputSystem.Player.SwitchToBullet2.performed += ctx => SwitchBulletTo(2);
        _inputSystem.Player.SwitchToBullet3.performed += ctx => SwitchBulletTo(3);
        _inputSystem.Player.SwitchToBullet4.performed += ctx => SwitchBulletTo(4);
    }

    public void Dispose()
    {
        _inputSystem.Player.SwitchToBullet0.performed -= ctx => SwitchBulletTo(0);
        _inputSystem.Player.SwitchToBullet1.performed -= ctx => SwitchBulletTo(1);
        _inputSystem.Player.SwitchToBullet2.performed -= ctx => SwitchBulletTo(2);
        _inputSystem.Player.SwitchToBullet3.performed -= ctx => SwitchBulletTo(3);
        _inputSystem.Player.SwitchToBullet4.performed -= ctx => SwitchBulletTo(4);

        _inputSystem.Disable();
    }

    private void SwitchBulletTo(int index) => SwitchBulletButtonClicked?.Invoke(index);

    public void SwitchTo(int bulletIndex) => SwitchBulletTo(bulletIndex);
}
