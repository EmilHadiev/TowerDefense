using System;
using UnityEngine;
using Zenject;

class MobileInput : IInput, ITickable, IMoveHandler
{
    private const int FirstTouch = 0;
    private const int NeededTouchCount = 0;
    private readonly IPlayerRotator _rotator;
    private readonly Joystick _rotateJoystick;
    private readonly Joystick _moveJoystick;

    private bool _isWork = true;
    private Vector3 _rotateDirection;
    
    public event Action Attacked;

    public MobileInput(IJoystickFactory joystickFactory, IPlayerRotator rotator)
    {
        _rotateJoystick = CreateJoystick(joystickFactory, AssetPath.MobileInputRotatorPath);
        _moveJoystick = CreateJoystick(joystickFactory, AssetPath.MobileInputMoverPath);
        _rotator = rotator;
    }

    public void Tick()
    {
        if (_isWork == false)
            return;

        HandleTouch();
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

    public Vector3 GetMoveDirection()
    {
        Vector3 direction = new Vector3(_moveJoystick.Horizontal, 0, _moveJoystick.Vertical);
        return direction;
    }

    private void HandleTouch()
    {
        if (Input.touchCount > NeededTouchCount)
        {
            Touch touch = Input.GetTouch(FirstTouch);

            switch (touch.phase)
            {
                case TouchPhase.Stationary:
                case TouchPhase.Moved:
                    Rotate();
                    InvokeAttack();
                    break;
            }
        }
    }

    private void Rotate()
    {
        Vector3 direction = new Vector3(_rotateJoystick.Horizontal, 0, _rotateJoystick.Vertical);
        _rotateDirection = direction;
        _rotator.Rotate(direction);
    }

    private void JoystickEnableToggle(bool isOn)
    {
        _rotateJoystick.gameObject.SetActive(isOn);
        _moveJoystick.gameObject.SetActive(isOn);
    }

    private void InvokeAttack()
    {
        if (_isWork == false || IsCanAttack())
            return;

        Attacked?.Invoke();
    }

    private bool IsCanAttack() => _rotateDirection == Vector3.zero;

    private Joystick CreateJoystick(IJoystickFactory factory, string path) => 
        factory.CreateJoystick(path);
}