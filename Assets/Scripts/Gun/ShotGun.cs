using UnityEngine;
using Zenject;

public class ShotGun : Gun
{
    private const int AttackRadius = 3;
    private readonly Collider[] _targets = new Collider[Constants.MaxEnemies];

    private PlayerStat _stat;

    [Inject]
    private void Constructor(PlayerStat stat)
    {
        _stat = stat;
    }

    public override void HandleAttack(Collider collider)
    {
        int count = Physics.OverlapSphereNonAlloc(collider.transform.position, AttackRadius, _targets, LayerMask.GetMask(Constants.EnemyMask));

        if (count == 0)
            return;

        for (int i = 0; i < count; i++)
            if (_targets[i].TryGetComponent(out IHealth health))
                health.TakeDamage(_stat.Damage / count);

        PhysicsDebug.DrawDebug(collider.transform.position, AttackRadius);
    }
}