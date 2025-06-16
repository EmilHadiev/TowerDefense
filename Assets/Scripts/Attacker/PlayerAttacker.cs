using System;
using UnityEngine;
using Zenject;

public class PlayerAttacker : IInitializable, IDisposable, ITickable, IAttackable
{
    private readonly IInput _input;
    private readonly PlayerStat _playerStat;
    private readonly IGunPlace _gunPlace;

    private float _timeAfterAttack;

    private bool _isAttacking;

    public event Action Attacked;
    public bool IsAttacking { get; private set; }

    public PlayerAttacker(IInput input, PlayerStat playerStat, IPlayer player)
    {
        _input = input;
        _isAttacking = true;
        _playerStat = playerStat;
        _gunPlace = player.GunPlace;
    }

    public void Initialize() => _input.Attacked += OnAttacked;

    public void Dispose() => _input.Attacked -= OnAttacked;

    public void Tick()
    {
        _timeAfterAttack += Time.deltaTime;
        if (_timeAfterAttack >= GetAttackSpeed())
            StartAttack();
    }

    private float GetAttackSpeed()
    {
        //return _playerStat.BonusAttackSpeed;
        return _gunPlace.CurrentGun.BaseAttackSpeed;
    }

    private void OnAttacked()
    {
        if (_isAttacking)
        {
            Attacked?.Invoke();
            IsAttacking = true;
            Debug.Log(_gunPlace.CurrentGun.BaseAttackSpeed);
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