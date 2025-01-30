using UnityEngine;

[RequireComponent(typeof(TriggerObserver))]
public class EnemyAttackZone : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private TriggerObserver _observer;

    private void OnValidate()
    {
        _enemy ??= GetComponentInParent<Enemy>();
        _observer ??= GetComponentInParent<TriggerObserver>();
    }

    private void OnEnable()
    {
        _observer.Entered += OnEntered;
        _observer.Exited += OnExited;
    }

    private void OnDisable()
    {
        _observer.Entered -= OnEntered;
        _observer.Exited -= OnExited;
    }

    private void OnEntered(Collider collider) => _enemy.StateMachine.SwitchTo<EnemyAttackState>();

    private void OnExited(Collider collider) => _enemy.StateMachine.SwitchTo<EnemyMoveState>();
}