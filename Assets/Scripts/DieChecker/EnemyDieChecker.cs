using UnityEngine;
using Zenject;

public class EnemyDieChecker : MonoBehaviour
{
    [SerializeField] private EnemyHealth _health;
    [SerializeField] private Enemy _enemy;

    private RewardSystem _rewardSystem;

    private bool _isDead;

    private void OnValidate()
    {
        _health ??= GetComponent<EnemyHealth>();
        _enemy ??= GetComponent<Enemy>();
    }

    private void OnEnable()
    {
        _health.Died += OnDied;
        Respawn();
    }

    private void OnDisable() => _health.Died -= OnDied;

    [Inject]
    private void Constructor(RewardSystem rewardSystem)
    {
        _rewardSystem = rewardSystem;
    }

    private void OnDied()
    {
        _rewardSystem.HandleEnemyDeath(_enemy.Stat.Point);
        RestoreHealth();
        SwitchToEmptyState();
        _isDead = true;

        gameObject.SetActive(false);
    }

    private void SwitchToEmptyState() => _enemy.StateMachine.SwitchTo<EmptyState>();

    private void RestoreHealth() => _health.AddHealth(_health.MaxHealth);

    private void Respawn()
    {
        if (_isDead)
        {
            SwitchToMoveState();
            _isDead = false;
        }
    }

    private void SwitchToMoveState() => _enemy.StateMachine.SwitchTo<EnemyMoveState>();
}