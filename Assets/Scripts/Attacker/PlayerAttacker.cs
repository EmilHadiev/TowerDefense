using System;
using UnityEngine;
using Zenject;

public class PlayerAttacker : IInitializable, IDisposable, ITickable, IAttackable
{
    private readonly IInput _input;
    private readonly PlayerStat _playerStat;

    private float _timeAfterAttack;

    private bool _isAttacking;

    public event Action Attacked;
    public bool IsAttacking { get; private set; }

    public PlayerAttacker(IInput input, PlayerStat playerStat)
    {
        _input = input;
        _isAttacking = true;
        _playerStat = playerStat;
    }

    public void Initialize() => _input.Attacked += OnAttacked;

    public void Dispose() => _input.Attacked -= OnAttacked;

    public void Tick()
    {
        _timeAfterAttack += Time.deltaTime;
        if (_timeAfterAttack >= _playerStat.BonusAttackSpeed)
            StartAttack();
    }

    private void OnAttacked()
    {
        if (_isAttacking)
        {
            Attacked?.Invoke();
            IsAttacking = true;
            ResetTimer();
            StopAttack();
        }
        else
        {
            IsAttacking = false;
        }
    }

    private void ResetTimer() => _timeAfterAttack = 0;

    private void StopAttack() => _isAttacking = false;

    private void StartAttack() => _isAttacking = true;
}