using UnityEngine;

[RequireComponent(typeof(TriggerObserver))]
public class EnemyAttackZone : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private TriggerObserver _observer;

    private IHealth _health;

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

    private void OnEntered(Collider collider) => _enemy.StateMachine.SwitchTo<EnemyAttackState>();

    private void OnExited(Collider collider) => _enemy.StateMachine.SwitchTo<EnemyMoveState>();

    private void OnDied() => _observer.UnLock();
}