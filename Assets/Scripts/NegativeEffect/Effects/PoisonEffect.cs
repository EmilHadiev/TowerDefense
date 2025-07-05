using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

public class PoisonEffect : INegativeEffect
{
    private const int BasePoisonDuration = 3;
    private const int Tick = 1000;
    private const float DamageImprover = 1.3f;

    private readonly EnemyRenderViewer _view;
    private readonly IGunPlace _gunPlace;
    private readonly IHealth _health;
    private readonly Color _poisonColor = Color.green;

    private CancellationTokenSource _poisonCts;
    private Color _startColor;
    private int _remainingTime;
    private int _damageMultiplier;
    private bool _isActive;

    public PoisonEffect(EnemyRenderViewer view, IGunPlace gunPlace, IHealth health)
    {
        _view = view;
        _health = health;
        _gunPlace = gunPlace;
    }

    public void StartEffect()
    {
        _damageMultiplier++;

        if (_isActive)
        {
            _remainingTime = BasePoisonDuration;
            return;
        }

        StartPoison();
        _isActive = true;

        _poisonCts = new CancellationTokenSource();
        PoisonEffectAsync(_poisonCts.Token).Forget();
    }

    public void StopEffect()
    {
        if (_isActive == false) return;

        _poisonCts?.Cancel();
        _poisonCts?.Dispose();
        _poisonCts = null;

        StopPoison();
    }

    private async UniTaskVoid PoisonEffectAsync(CancellationToken ct)
    {
        try
        {
            while (_remainingTime > 0 && ct.IsCancellationRequested == false)
            {
                await UniTask.Delay(Tick, cancellationToken: ct);
                if (ct.IsCancellationRequested) break;

                _remainingTime--;
                _health.TakeDamage(CalculateDamage());
            }

            if (ct.IsCancellationRequested == false)
                StopPoison();
        }
        catch (OperationCanceledException)
        {
            
        }
    }

    private int CalculateDamage() => (int)(_gunPlace.CurrentGun.Damage * DamageImprover * _damageMultiplier);

    private void StartPoison()
    {
        _startColor = _view.Color;
        _view.SetColor(_poisonColor);
        _remainingTime = BasePoisonDuration;
    }

    private void StopPoison()
    {
        _view.SetColor(_startColor);
        _damageMultiplier = 1;
        _isActive = false;
    }
}