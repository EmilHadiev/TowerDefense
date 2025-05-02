using System;
using UnityEngine;
using Zenject;

public class DesktopInput : IInput, ITickable
{
    private const int AttackButton = 0;

    private readonly IPlayerRotator _rotator;

    private bool _isWork = true;

    public event Action Attacked;

    public DesktopInput(IPlayerRotator rotator)
    {
        _rotator = rotator;       
    }

    public void Tick()
    {
        if (_isWork == false)
            return;

        Attack();
        Rotate();
    }

    public void Continue() => _isWork = true;

    public void Stop() => _isWork = false;

    private void Attack()
    {
        if (_isWork == false)
            return;

        if (Input.GetMouseButton(AttackButton))
            Attacked?.Invoke();
    }

    private void Rotate() => _rotator.Rotate(Input.mousePosition);
}