using UnityEngine;
using Zenject;

public class EnemyDieChecker : MonoBehaviour
{
    [SerializeField] private EnemyHealth _health;
    [SerializeField] private Enemy _enemy;

    private ICoinStorage _coinStorage;
    private EnemyCounter _counter;
    private bool _isDead;

    private void OnValidate()
    {
        _health ??= GetComponent<EnemyHealth>();
        _enemy ??= GetComponent<Enemy>();
    }

    private void OnEnable()
    {
        _health.Died += OnDied;
        Spawned();
    }

    private void OnDisable() => _health.Died -= OnDied;

    [Inject]
    private void Constructor(ICoinStorage coinStorage, EnemyCounter counter)
    {
        _coinStorage = coinStorage;
        _counter = counter;
    }

    private void OnDied()
    {
        _coinStorage.Add(_enemy.Stat.Point);
        _counter.Remove();
        _health.AddHealth(_health.MaxHealth);
        _enemy.StateMachine.SwitchTo<EmptyState>();
        _isDead = true;

        gameObject.SetActive(false);
    }

    private void Spawned()
    {
        if (_isDead)
        {
            _enemy.StateMachine.SwitchTo<EnemyMoveState>();
            _isDead = false;
        }
    }
}