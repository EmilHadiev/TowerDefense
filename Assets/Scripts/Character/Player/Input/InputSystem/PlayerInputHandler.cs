using System;
using System.Linq;
using UnityEngine.InputSystem;
using Zenject;

class PlayerInputHandler : IInputHandler, IInitializable, IDisposable
{
    private readonly InputSystem _inputSystem;

    private readonly InputAction[] _bulletSwitchActions;
    private readonly Action<InputAction.CallbackContext>[] _bulletsCallback;

    public event Action<int> SwitchBulletButtonClicked;

    public PlayerInputHandler()
    {
        _inputSystem = new InputSystem();
        _bulletSwitchActions = new BulletSwitcherHandler(_inputSystem).Actions.ToArray();
        _bulletsCallback = new Action<InputAction.CallbackContext>[_bulletSwitchActions.Length];
    }

    public void Initialize()
    {
        _inputSystem.Enable();

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

        _inputSystem.Disable();
    }

    private void SwitchBulletTo(int index) => SwitchBulletButtonClicked?.Invoke(index);

    public void SwitchTo(int bulletIndex) => SwitchBulletTo(bulletIndex);
}