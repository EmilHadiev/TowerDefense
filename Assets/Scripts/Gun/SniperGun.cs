using UnityEngine;
using Zenject;

public class SniperGun : Gun
{
    private const int CriticalChance = 25;
    private PlayerStat _stat;
    private ICameraProvider _camera;

    [Inject]
    private void Constructor(PlayerStat stat, ICameraProvider cameraProvider)
    {
        _stat = stat;
        _camera = cameraProvider;
    }

    public override void HandleAttack(Collider collider)
    {
        if (collider.TryGetComponent(out IHealth health))
        {
            float criticalDamage = GetCriticalStrike();
            health.TakeDamage(criticalDamage);

            if (criticalDamage > 0)
                _camera.Punch();
        }
    }

    private float GetCriticalStrike()
    {
        int randomValue = Random.Range(0, 100);
        return CriticalChance >= randomValue ? _stat.Damage * CriticalChance + CriticalChance : 0;
    }
}