using UnityEngine;
using Zenject;

public class ShotGun : Gun
{
    private PlayerStat _stat;
    private const int AttackRadius = 3;
    private readonly Collider[] _targets = new Collider[Constants.MaxEnemies];

    private LazyInject<IAttackable> _attackable;

    [Inject]
    private void Constructor(LazyInject<IAttackable> attackable, PlayerStat stat)
    {
        _attackable = attackable;
        _stat = stat;
    }

    private void Start()
    {
        _attackable.Value.Attacked += OnAttacked;
    }

    private void OnDestroy()
    {
        _attackable.Value.Attacked -= OnAttacked;
    }

    private void OnAttacked()
    {
        Debug.Log("¿“¿ ¿!");

        int count = Physics.OverlapSphereNonAlloc(GetPosition(), AttackRadius, _targets, LayerMask.GetMask(Constants.EnemyMask));

        for (int i = 0; i < count; i++)
            if (_targets[i].TryGetComponent(out IHealth health))
                health.TakeDamage(_stat.Damage / count);
    }

    private Vector3 GetPosition()
    {
        Vector3 extraRange = transform.forward * 3;
        return transform.position - extraRange;
    }
}
