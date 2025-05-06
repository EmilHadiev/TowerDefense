using System;
using UnityEngine;
using Zenject;

class MobileInput : IInput, ITickable
{
    private const int FirstTouch = 0;

    private readonly IPlayerRotator _rotator;
    private readonly Joystick _joystick;

    private bool _isWork = true;
    
    public event Action Attacked;
    public event Action<Vector3> Moving;

    public MobileInput(IJoystickFactory joystickFactory, IPlayerRotator rotator)
    {
        _joystick = joystickFactory.CreateJoystick();
        _rotator = rotator;
    }

    public void Tick()
    {
        if (_isWork == false)
            return;

        HandleTouch();
    }

    private void HandleTouch()
    {
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

    private void Move()
    {

    }
}