using System;
using UnityEngine;
using Zenject;

public class PlayerAttacker : IInitializable, IDisposable, ITickable, IAttackable
{
    private readonly IInput _input;
    private readonly PlayerStat _playerStat;
    private readonly ISoundContainer _soundContainer;

    private float _timeAfterAttack;

    private bool _isAttacking;

    public event Action Attacked;

    public PlayerAttacker(IInput input, PlayerStat playerStat, ISoundContainer soundContainer)
    {
        _input = input;
        _isAttacking = true;
        _playerStat = playerStat;
        _soundContainer = soundContainer;
    }

    public void Initialize() => _input.Attacked += OnAttacked;

    public void Dispose() => _input.Attacked -= OnAttacked;

    private void OnAttacked()
    {
        if (_isAttacking)
        {
            Debug.Log("Атака!");
            Attacked?.Invoke();
            ResetTimer();
            StopAttack();
            PlaySound();
        }
    }

    private void ResetTimer() => _timeAfterAttack = 0;

    private void PlaySound() => _soundContainer.Play();

    private void StopAttack() => _isAttacking = false;

    private void StartAttack() => _isAttacking = true;

    public void Tick()
    {
        _timeAfterAttack += Time.deltaTime;
        if (_timeAfterAttack >= _playerStat.AttackSpeed)
            StartAttack();
    }
}