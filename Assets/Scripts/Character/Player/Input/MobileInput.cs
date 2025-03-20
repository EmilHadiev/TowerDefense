using System;
using UnityEngine;
using Zenject;

class MobileInput : IInput, ITickable
{
    private const int FirstTouch = 0;

    private readonly PlayerRotator _rotator;
    private readonly Joystick _joystick;

    private bool _isWork = true;
    
    public event Action Attacked;

    public MobileInput(IPlayer player, IInstantiator instantitator)
    {
        _joystick = InstantiateJoystick(instantitator);
        _rotator = new MobilePlayerRotator(player, _joystick);
    }

    public Joystick InstantiateJoystick(IInstantiator instantitator)
    {
        PlayerUI playerUI = GameObject.FindAnyObjectByType<PlayerUI>();

        if (playerUI.TryGetComponent(out RectTransform parent) == false)
            throw new ArgumentNullException(nameof(parent));

        Joystick joystick = instantitator.InstantiatePrefabResourceForComponent<Joystick>(AssetPath.MobileInputPath, parent);

        return joystick;
    }

    public void Tick()
    {
        if (_isWork == false)
            return;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(FirstTouch);

            switch (touch.phase)
            {
                case TouchPhase.Stationary:
                case TouchPhase.Moved:
                    _rotator.Rotate(new Vector3(_joystick.Horizontal, 0, _joystick.Vertical));
                    StartAttack();
                    break;
            }
        }
    }

    public void Continue()
    {
        _isWork = true;
        JoystickEnableToggle(_isWork);
    }

    public void Stop()
    {
        _isWork = false;
        JoystickEnableToggle(_isWork);
    }

    private void JoystickEnableToggle(bool isOn) => _joystick.gameObject.SetActive(isOn);

    private void StartAttack()
    {
        if (_isWork == false)
            return;

        Attacked?.Invoke();
    } 
}
