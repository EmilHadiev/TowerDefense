using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

public class BloodyLord : EliteEnemy
{
    private const int Radius = 10;
    private const int Interval = 2000;
    private const float HealthPercentage = 0.1f;

    private readonly Collider[] _hits = new Collider[Constants.MaxEnemies];
    private readonly LayerMask _mask;
    private readonly ParticleView _bloodyAura;
    private readonly Transform _transform;
    private readonly IEnemySoundContainer _soundContainer;

    private CancellationTokenSource _healCts;

    public BloodyLord(Color viewColor, EnemyRenderViewer renderViewer, ParticleView bloodyAura, Transform transform, IEnemySoundContainer enemySoundContainer) 
        : base(viewColor, renderViewer)
    {
        _mask = LayerMask.GetMask("Enemy");
        _transform = transform;
        _bloodyAura = bloodyAura;
        _soundContainer = enemySoundContainer;
        _bloodyAura.Stop();
    }

    public override void ActivateAbility()
    {
        StartHealingCycle().Forget();
    }

    private async UniTaskVoid StartHealingCycle()
    {
        DeactivateAbility();
        _bloodyAura.Play();

        _healCts = new CancellationTokenSource();
        var ct = _healCts.Token;

        try
        {
            while (ct.IsCancellationRequested == false)
            {
                HealAllies();
                await UniTask.Delay(Interval, cancellationToken: ct);
            }
        }
        catch (OperationCanceledException)
        {
            
        }
    }

    public override void DeactivateAbility()
    {
        _healCts?.Cancel();
        _healCts?.Dispose();
        _healCts = null;
        _bloodyAura.Stop();
    }

    private void HealAllies()
    {
        int enemiesCount = Physics.OverlapSphereNonAlloc(_transform.position, Radius, _hits, _mask);

        if (enemiesCount == 0)
            return;

        PhysicsDebug.DrawDebug(_transform.position, Radius, 1, Color.yellow);

        _soundContainer.Play(SoundName.BloodHealing);

        for (int i = 0; i < enemiesCount; i++)
        {
            if (_hits[i].TryGetComponent(out EnemyHealth health))
            {
                health.AddHealth(health.MaxHealth * HealthPercentage);
            }
        }
    }

    public override void SetColor()
    {
        base.SetColor();
        _bloodyAura.SetColor(ViewColor);
    }
}