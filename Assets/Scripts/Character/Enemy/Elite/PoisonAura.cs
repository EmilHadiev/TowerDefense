using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
using Zenject;

public class PoisonAura : MonoBehaviour
{
    [SerializeField] private ParticleView _poisonAura;  

    private const int Radius = 5;
    private const int Interval = 2000;
    private const int DamageReducer = 2;

    private Collider[] _hits = new Collider[1];

    private EnemyStat _enemyStat;
    private LayerMask _mask;
    private IPlayer _player;
    private IEnemySoundContainer _soundContainer;
    private CancellationTokenSource _attackCts;

    private bool _isSeparated;

    private void Awake()
    {
        _mask = LayerMask.GetMask("Player");
        _enemyStat = GetComponentInParent<Enemy>().Stat;
    }

    private void OnDestroy()
    {
        StopAttackCycle();
    }

    [Inject]
    private void Constructor(IPlayer player, IEnemySoundContainer soundContainer)
    {
        _player = player;
        _soundContainer = soundContainer;
    }

    public void Activate()
    {
        _poisonAura.Play();
        StartAttackCycle().Forget();
    }

    public void Deactivate()
    {
        StopAttackCycle();
        _poisonAura.Stop();
    }

    public void Separate()
    {
        _mask = LayerMask.GetMask("Player", "Enemy");
        _hits = new Collider[Constants.MaxEnemies];
        transform.parent = null;
        _isSeparated = true;
        Activate();
    }

    private async UniTaskVoid StartAttackCycle()
    {
        StopAttackCycle();

        _attackCts = new CancellationTokenSource();
        var ct = _attackCts.Token;

        try
        {
            while (ct.IsCancellationRequested == false)
            {
                if (_isSeparated)
                    AttackAll();
                else
                    AttackPlayer();
                await UniTask.Delay(Interval, cancellationToken: ct);
            }
        }
        catch (OperationCanceledException)
        {
            // Корректная остановка атаки
        }
    }

    private void StopAttackCycle()
    {
        _attackCts?.Cancel();
        _attackCts?.Dispose();
        _attackCts = null;
    }

    private void AttackPlayer()
    {
        int targetCount = GetTargetCountAndDrawDebug();

        if (targetCount == 0)
        {
            ClearTargets();
            return;
        }

        _soundContainer.Play(SoundName.PoisonAura);
        _player.Health.TakeDamage(GetDamage());
    }

    private void AttackAll()
    {
        int targetCount = GetTargetCountAndDrawDebug();

        if (targetCount == 0)
        {
            ClearTargets();
            return;
        }

        _soundContainer.Play(SoundName.PoisonAura);
        for (int i = 0; i < targetCount; i++)
            if (_hits[i].TryGetComponent(out IHealth health))
                health.TakeDamage(GetDamage());
    }

    private int GetTargetCountAndDrawDebug()
    {
        int targetCount = Physics.OverlapSphereNonAlloc(transform.position, Radius, _hits, _mask);
        PhysicsDebug.DrawDebug(transform.position, Radius, 1, Color.blue);
        return targetCount;
    }

    private float GetDamage()
    {
        return _enemyStat.Damage / DamageReducer;
    }

    private void ClearTargets()
    {
        for (int i = 0; i < _hits.Length; i++)
            _hits[i] = null;
    }
}
