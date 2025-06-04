using UnityEngine;
using Zenject;

public class ShotGun : Gun, IGunAbility
{
    private const int AttackRadius = 3;
    private readonly Collider[] _targets = new Collider[Constants.MaxEnemies];

    private PlayerStat _stat;

    [Inject]
    private void Constructor( PlayerStat stat)
    {
        _stat = stat;
    }

    public void Activate(Collider target)
    {
        int count = Physics.OverlapSphereNonAlloc(target.transform.position, AttackRadius, _targets, LayerMask.GetMask(Constants.EnemyMask));

        for (int i = 0; i < count; i++)
            if (_targets[i].TryGetComponent(out IHealth health))
                health.TakeDamage(_stat.Damage / count);

        PhysicsDebug.DrawDebug(target.transform.position, AttackRadius);
    }
}