using System;
using UnityEngine;
using Zenject;

public abstract class BaseMobileInput : IInput, ITickable
{
    protected const int FirstTouch = 0;
    protected const int NeededTouchCount = 0;

    protected bool IsWork = true;
    protected readonly Joystick AttackJoystick;
    protected Vector3 RotateDirection;

    protected readonly IPlayerRotator Rotator;

    public event Action Attacked;

    public BaseMobileInput(IJoystickFactory joystickFactory, IPlayerRotator rotator, string rotateJoystickPath)
    {
        Rotator = rotator;
        AttackJoystick = CreateJoystick(joystickFactory, rotateJoystickPath);
    }

    public abstract void Tick();

    public virtual void Continue()
    {
        IsWork = true;
        AttackJoystick.gameObject.SetActive(true);
    }

    public virtual void Stop()
    {
        IsWork = false;
        AttackJoystick.gameObject.SetActive(false);
    }

    protected virtual void HandleTouch()
    {
        if (Input.touchCount > NeededTouchCount)
        {
            Touch touch = Input.GetTouch(FirstTouch);

            switch (touch.phase)
            {
                case TouchPhase.Stationary:
                case TouchPhase.Moved:
                    Rotate();
                    TryAttack();
                    break;
            }
        }
    }

    protected virtual void Rotate()
    {
        RotateDirection = new Vector3(AttackJoystick.Horizontal, 0, AttackJoystick.Vertical);
        Rotator.Rotate(RotateDirection);
    }

    protected virtual void TryAttack()
    {
        if (IsWork && !IsCanAttack())
            Attacked?.Invoke();
    }

    protected virtual bool IsCanAttack() => RotateDirection == Vector3.zero;

    protected Joystick CreateJoystick(IJoystickFactory factory, string path) =>
        factory.CreateJoystick(path);
}