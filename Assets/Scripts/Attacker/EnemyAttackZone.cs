using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

[RequireComponent(typeof(TriggerObserver))]
public class EnemyAttackZone : MonoBehaviour
{
    [SerializeField] private TriggerObserver _observer;
    [SerializeField] private Enemy _enemy;

    private const int WaitingTimeMs = 1000;

    private IHealth _health;
    private IHealth _targetHealth;
    private IStateSwitcher _stateSwitcher;

    private CancellationTokenSource _targetCheckCts;

    private void OnValidate()
    {
        _enemy ??= GetComponentInParent<Enemy>();
        _observer ??= GetComponentInParent<TriggerObserver>();
    }

    private void Awake() => _health = _enemy.GetComponent<IHealth>();

    private void Start() => _stateSwitcher = _enemy.StateMachine;

    private void OnEnable()
    {
        _observer.Entered += OnEntered;
        _observer.Exited += OnExited;
        _health.Died += OnDied;
    }

    private void OnDisable()
    {
        _observer.Entered -= OnEntered;
        _observer.Exited -= OnExited;
        _health.Died -= OnDied;

        StopTargetCheck();
    }

    public void Attack()
    {
        _stateSwitcher.SwitchTo<EnemyAttackState>();
        StopTargetCheck();

        _targetCheckCts = new CancellationTokenSource();
        CheckTargetAliveAsync(_targetCheckCts.Token).Forget();
    }

    private void StopTargetCheck()
    {
        _targetCheckCts?.Cancel();
        _targetCheckCts?.Dispose();
        _targetCheckCts = null;
    }

    public void Move()
    {
        StopTargetCheck();
        _stateSwitcher.SwitchTo<EnemyMoveState>();
        _observer.UnLock();
    }

    private async UniTaskVoid CheckTargetAliveAsync(CancellationToken ct)
    {
        try
        {
            while (_targetHealth.IsAlive && ct.IsCancellationRequested == false)
            {
                await UniTask.Delay(WaitingTimeMs, cancellationToken: ct);
            }

            if (ct.IsCancellationRequested == false)
            {
                Move();
            }
        }
        catch (Exception)
        {
            
        }
    }

    private void OnEntered(Collider collider)
    {
        _targetHealth = collider.GetComponent<IHealth>();
        Attack();
    }

    private void OnExited(Collider collider) => Move();

    private void OnDied()
    {
        _observer.UnLock();
        StopTargetCheck();
    }
}