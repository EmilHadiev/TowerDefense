using UnityEngine;
using Zenject;

public class SniperGun : Gun, IGunAbility
{
    private const int CriticalChance = 20;
    private PlayerStat _stat;

    [Inject]
    private void Constructor(PlayerStat stat)
    {
        _stat = stat;
    }

    public void Activate(Collider target)
    {
        if (target.TryGetComponent(out IHealth health))
            health.TakeDamage(GetCriticalStrike());
    }

    private float GetCriticalStrike()
    {
        int randomValue = Random.Range(0, 100);
        return CriticalChance >= randomValue ? _stat.Damage : 0;
    }
}