using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

public class FreezingEffect : INegativeEffect
{
    private const int PercentageSlowdown = 50;
    private const int SlowdownDuration = 2000; // 2 секунды в миллисекундах

    private readonly Property _speed;
    private readonly EnemyRenderViewer _view;
    private readonly Color _freezeColor = Color.blue;

    private Color _startColor;
    private float _defaultSpeed;
    private CancellationTokenSource _effectCts;

    public FreezingEffect(Property speed, EnemyRenderViewer view)
    {
        _speed = speed;
        _defaultSpeed = _speed.Value;
        _view = view;
    }

    public void StartEffect()
    {
        StopEffect();

        _effectCts = new CancellationTokenSource();
        SlowdownEffectAsync(_effectCts.Token).Forget();
    }

    public void StopEffect()
    {
        _effectCts?.Cancel();
        _effectCts?.Dispose();
        _effectCts = null;

        StopFreeze();
    }

    private async UniTaskVoid SlowdownEffectAsync(CancellationToken ct)
    {
        try
        {
            StartFreeze();
            await UniTask.Delay(SlowdownDuration, cancellationToken: ct);
            StopFreeze();
        }
        catch (OperationCanceledException)
        {
            
        }
    }

    private void StartFreeze()
    {
        _startColor = _view.Color;
        _view.SetColor(_freezeColor);
        _speed.Value = GetFreezeSpeed();
    }

    private void StopFreeze()
    {
        _view.SetColor(_startColor);
        _speed.Value = _defaultSpeed;
    }

    private float GetFreezeSpeed() => _speed.Value / 100 * PercentageSlowdown;
}