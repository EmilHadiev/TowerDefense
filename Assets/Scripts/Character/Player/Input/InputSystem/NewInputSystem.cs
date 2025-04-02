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
        _inputSystem.Player.SwitchToBullet5.performed += ctx => SwitchBulletTo(5);
        _inputSystem.Player.SwitchToBullet6.performed += ctx => SwitchBulletTo(6);
        _inputSystem.Player.SwitchToBullet7.performed += ctx => SwitchBulletTo(7);
        _inputSystem.Player.SwitchToBullet8.performed += ctx => SwitchBulletTo(8);
        _inputSystem.Player.SwitchToBullet9.performed += ctx => SwitchBulletTo(9);
    }

    public void Dispose()
    {
        _inputSystem.Player.SwitchToBullet0.performed -= ctx => SwitchBulletTo(0);
        _inputSystem.Player.SwitchToBullet1.performed -= ctx => SwitchBulletTo(1);
        _inputSystem.Player.SwitchToBullet2.performed -= ctx => SwitchBulletTo(2);
        _inputSystem.Player.SwitchToBullet3.performed -= ctx => SwitchBulletTo(3);
        _inputSystem.Player.SwitchToBullet4.performed -= ctx => SwitchBulletTo(4);
        _inputSystem.Player.SwitchToBullet5.performed -= ctx => SwitchBulletTo(5);
        _inputSystem.Player.SwitchToBullet6.performed -= ctx => SwitchBulletTo(6);
        _inputSystem.Player.SwitchToBullet7.performed -= ctx => SwitchBulletTo(7);
        _inputSystem.Player.SwitchToBullet8.performed -= ctx => SwitchBulletTo(8);
        _inputSystem.Player.SwitchToBullet9.performed -= ctx => SwitchBulletTo(9);

        _inputSystem.Disable();
    }

    private void SwitchBulletTo(int index) => SwitchBulletButtonClicked?.Invoke(index);

    public void SwitchTo(int bulletIndex) => SwitchBulletTo(bulletIndex);
}
