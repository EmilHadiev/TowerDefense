using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
using Zenject;

public class BlackHoleAura : MonoBehaviour
{    
    private const int AttractionForce = 10;
    private const int MinDistance = 5;
    private const int Interval = 100;

    private IPlayer _player;
    private IEnemySoundContainer _soundContainer;
    private CancellationTokenSource _attractionCts;
    private bool _isActive;

    [Inject]
    private void Constructor(IPlayer player, IEnemySoundContainer soundContainer)
    {
        _player = player;
        _soundContainer = soundContainer;
    }

    public void Activate()
    {
        if (_isActive) 
            return;

        _isActive = true;
        StartAttraction().Forget();
    }

    public void Deactivate()
    {
        if (_isActive == false) 
            return;

        _isActive = false;
        StopAttraction();
    }

    private async UniTaskVoid StartAttraction()
    {
        _attractionCts = new CancellationTokenSource();
        var ct = _attractionCts.Token;

        try
        {
            while (_isActive && ct.IsCancellationRequested == false)
            {
                Attract();
                await UniTask.Delay(Interval, cancellationToken: ct);
            }
        }
        catch (OperationCanceledException)
        {
            
        }
    }

    private void StopAttraction()
    {
        _attractionCts?.Cancel();
        _attractionCts?.Dispose();
        _attractionCts = null;
    }

    private void Attract()
    {
        Vector3 direction = transform.position - _player.Transform.position;
        float distance = direction.magnitude;

        if (distance <= MinDistance)
            return;

        _soundContainer.Play(SoundName.BlackHoleAura);
        Vector3 force = direction.normalized * AttractionForce * Time.deltaTime;
        force.y = 0;
        _player.Transform.position += force;
    }

    private void OnDestroy()
    {
        Deactivate();
    }
}