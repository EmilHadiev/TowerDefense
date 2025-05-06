using System;
using UnityEngine.InputSystem;
using Zenject;

public class BulletSwitchHandler : IBulletSwitchHandler, IInitializable, IDisposable
{
    private readonly InputSystem _inputSystem;

    private readonly InputAction[] _bulletSwitchActions;
    private readonly Action<InputAction.CallbackContext>[] _bulletsCallback;

    public event Action<int> SwitchBulletButtonClicked;

    public BulletSwitchHandler(IPlayerInputSystem playerInputSystem)
    {
        _inputSystem = playerInputSystem.InputSystem;
        _bulletSwitchActions = GetInputActions(_inputSystem);
        _bulletsCallback = new Action<InputAction.CallbackContext>[_bulletSwitchActions.Length];
    }

    public void Initialize()
    {
        for (int i = 0; i < _bulletSwitchActions.Length; i++)
        {
            int tmpIndex = i;
            _bulletsCallback[i] = cxt => SwitchBulletTo(tmpIndex);
            _bulletSwitchActions[i].performed += _bulletsCallback[i];
        }
    }

    public void Dispose()
    {
        for (int i = 0; i < _bulletSwitchActions.Length; i++)
            _bulletSwitchActions[i].performed -= _bulletsCallback[i];
    }

    private void SwitchBulletTo(int index) => SwitchBulletButtonClicked?.Invoke(index);

    public void SwitchTo(int bulletIndex) => SwitchBulletTo(bulletIndex);

    private InputAction[] GetInputActions(InputSystem inputSystem)
    {
        return new InputAction[]
        {
            inputSystem.Player.SwitchToBullet0,
            inputSystem.Player.SwitchToBullet1,
            inputSystem.Player.SwitchToBullet2,
            inputSystem.Player.SwitchToBullet3,
            inputSystem.Player.SwitchToBullet4,
            inputSystem.Player.SwitchToBullet5,
            inputSystem.Player.SwitchToBullet6,
            inputSystem.Player.SwitchToBullet7,
            inputSystem.Player.SwitchToBullet8,
            inputSystem.Player.SwitchToBullet9,
        };
    }
}