using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerAnimator : CharacterAnimator
{
    private IMoveHandler _moveHandler;
    private IAttackable _attackable;

    private void OnEnable() => _attackable.Attacked += OnAttacked;

    private void OnDisable() => _attackable.Attacked -= OnAttacked;

    [Inject]
    private void Constructor(IMoveHandler moveHandler, IAttackable attackable)
    {
        _moveHandler = moveHandler;
        _attackable = attackable;
    }

    private void Update()
    {
        Running();
        //Attacking();
    }

    private void Attacking()
    {
        if (_attackable.IsAttacking)
            StartAttacking();
        else
            StopAttacking();
    }

    private void Running()
    {
        if (_moveHandler.GetMoveDirection() != Vector3.zero)
            StartRunning();
        else
            StopRunning();
    }

    private void OnAttacked()
    {
        
    }
}
