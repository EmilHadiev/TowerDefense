using UnityEngine;
using Zenject;

public class EnemyDieChecker : MonoBehaviour
{
    [SerializeField] private EnemyHealth _health;
    [SerializeField] private Enemy _enemy;

    private const int Point = 1;

    private ICoinStorage _coinStorage;

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
    private void Constructor(ICoinStorage coinStorage) => _coinStorage = coinStorage;

    private void OnDied()
    {
        _coinStorage.Add(_enemy.Stat.Point);
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