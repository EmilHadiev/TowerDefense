using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(TriggerObserver))]
public class EnemyAttackZone : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private TriggerObserver _observer;

    private const int WaitingTime = 1;

    private IHealth _health;
    private IHealth _targetHealth;

    private Coroutine _targetAliveCheck;
    private readonly WaitForSeconds _delay = new WaitForSeconds(WaitingTime);

    private void OnValidate()
    {
        _enemy ??= GetComponentInParent<Enemy>();
        _observer ??= GetComponentInParent<TriggerObserver>();
    }

    private void Awake() => _health = _enemy.GetComponent<IHealth>();

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
        _enemy.StateMachine.SwitchTo<EnemyAttackState>();
        StopAliveCheck();

        _targetAliveCheck = StartCoroutine(CheckTargetAlive());
    }

    private void StopAliveCheck()
    {
        if (_targetAliveCheck != null)
            StopCoroutine(_targetAliveCheck);
    }

    public void Move()
    {
        StopAliveCheck();
        _enemy.StateMachine.SwitchTo<EnemyMoveState>();
        _observer.UnLock();
    }

    private void OnEntered(Collider collider)
    {
        _targetHealth = collider.GetComponent<IHealth>();
        Attack();
    }
    private void OnExited(Collider collider) => Move();

    private void OnDied() => _observer.UnLock();

    private IEnumerator CheckTargetAlive()
    {
        while (_targetHealth.IsAlive)
            yield return _delay;

        _targetAliveCheck = null;
        Move();
    }
}