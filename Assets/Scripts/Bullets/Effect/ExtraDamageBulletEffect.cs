using UnityEngine;

public class ExtraDamageBulletEffect : IBulletEffectHandler
{
    private const float AdditionalPercentageDamage = 50;
    private readonly IGunPlace _gunPlace;

    public ExtraDamageBulletEffect(IGunPlace gunPlace)
    {
        _gunPlace = gunPlace;
    }

    public void HandleEffect(Collider enemy)
    {
        if (enemy.TryGetComponent(out IHealth health))
            health.TakeDamage(GetAdditionalDamage());
    }

    private float GetAdditionalDamage() => GetDamage() / 100 * AdditionalPercentageDamage;

    private float GetDamage() => _gunPlace.CurrentGun.Damage;
}