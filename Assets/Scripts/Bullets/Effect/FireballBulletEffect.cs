using UnityEngine;

public class FireballBulletEffect : IBulletEffectHandler
{
    private const float AdditionalPercentageDamage = 50;
    private readonly BulletData _data;

    public FireballBulletEffect(BulletData data)
    {
        _data = data;
    }

    public void HandleEffect(Collider enemy)
    {
        if (enemy.TryGetComponent(out IHealth health))
            health.TakeDamage(GetAdditionalDamage());
    }

    private float GetAdditionalDamage() => _data.Damage / 100 * AdditionalPercentageDamage;
}