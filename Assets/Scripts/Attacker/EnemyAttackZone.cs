using System.Collections;
using UnityEngine;

[RequireComponent(typeof(TriggerObserver))]
public class EnemyAttackZone : MonoBehaviour
{
    [SerializeField] private TriggerObserver _observer;
    [SerializeField] private Enemy _enemy;

    private const int WaitingTime = 1;

    private IHealth _health;
    private IHealth _targetHealth;
    private IStateSwitcher _stateSwitcher;

    private Coroutine _targetCheckCoroutine;
    private readonly WaitForSeconds _delay = new WaitForSeconds(WaitingTime);

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
    }

    public void Attack()
    {
        _stateSwitcher.SwitchTo<EnemyAttackState>();
        StopTargetCheck();

        _targetCheckCoroutine = StartCoroutine(CheckTargetAlive());
    }

    private void StopTargetCheck()
    {
        if (_targetCheckCoroutine != null)
            StopCoroutine(_targetCheckCoroutine);
    }

    public void Move()
    {
        StopTargetCheck();
        _stateSwitcher.SwitchTo<EnemyMoveState>();
        _observer.UnLock();
    }

    private IEnumerator CheckTargetAlive()
    {
        while (_targetHealth.IsAlive)
            yield return _delay;

        Move();
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