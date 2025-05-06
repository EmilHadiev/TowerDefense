using System;
using UnityEngine;
using Zenject;

public class DesktopInput : IInput, ITickable
{
    private const int AttackButton = 0;

    private readonly IPlayerRotator _rotator;
    private readonly IMoveHandler _moveHandler;

    private bool _isWork = true;

    public event Action Attacked;
    public event Action<Vector3> Moving;

    public DesktopInput(IPlayerRotator rotator, IMoveHandler moveHandler)
    {
        _rotator = rotator;   
        _moveHandler = moveHandler;
    }

    public void Tick()
    {
        if (_isWork == false)
            return;

        Attack();
        Rotate();
        Move();
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

    private void Move()
    {
        Debug.Log(_moveHandler.GetMoveDirection());
    }
}