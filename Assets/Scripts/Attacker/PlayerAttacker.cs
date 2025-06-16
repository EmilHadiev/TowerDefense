using System;
using UnityEngine;
using Zenject;

public class PlayerAttacker : IInitializable, IDisposable, ITickable, IAttackable
{
    private const float AttackSpeedFactor = 0.99f;

    private readonly IInput _input;
    private readonly PlayerStat _playerStat;
    private readonly IGunPlace _gunPlace;

    private float _timeAfterAttack;
    private bool _isAttacking;

    public float AttackSpeed { get; private set; }   
    public bool IsAttacking { get; private set; }

    public event Action Attacked;

    public PlayerAttacker(IInput input, PlayerStat playerStat, IPlayer player)
    {
        _input = input;
        _isAttacking = true;
        _playerStat = playerStat;
        _gunPlace = player.GunPlace;
    }

    public void Initialize()
    {
        _input.Attacked += OnAttacked;
        _gunPlace.GunSwitched += OnGunSwitched;
    }

    public void Dispose()
    {
        _input.Attacked -= OnAttacked;
        _gunPlace.GunSwitched -= OnGunSwitched;
    }

    public void Tick()
    {
        _timeAfterAttack += Time.deltaTime;
        if (_timeAfterAttack >= AttackSpeed)
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

    private void OnGunSwitched(IGun gun)
    {
        float gunSpeed = gun.BaseAttackSpeed;
        int playerSpeed = _playerStat.BonusAttackSpeed;
        AttackSpeed = gunSpeed * MathF.Pow(AttackSpeedFactor, playerSpeed);

        Debug.Log("Скорость атаки игрока: " + playerSpeed);
        Debug.Log("Скорость атаки оружия: " + gunSpeed);
        Debug.Log("Текущая скорость атаки: " + AttackSpeed);
        Debug.Log(new string('-', 10));
    }
}