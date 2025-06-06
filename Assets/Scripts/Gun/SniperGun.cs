using UnityEngine;
using Zenject;

public class SniperGun : Gun
{
    private const int CriticalChance = 100;
    private PlayerStat _stat;

    [Inject]
    private void Constructor(PlayerStat stat)
    {
        _stat = stat;
    }

    public override void HandleAttack(Collider collider)
    {
        if (collider.TryGetComponent(out IHealth health))
            health.TakeDamage(GetCriticalStrike());
    }

    private float GetCriticalStrike()
    {
        int randomValue = Random.Range(0, 100);
        return CriticalChance >= randomValue ? _stat.Damage * CriticalChance + CriticalChance : 0;
    }
}