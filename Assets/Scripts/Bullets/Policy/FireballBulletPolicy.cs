using UnityEngine;

public class FireballBulletPolicy : IBulletPolicy
{
    private const float AdditionalPercentageDamage = 50;
    private readonly BulletData _data;

    public FireballBulletPolicy(BulletData data)
    {
        _data = data;
    }

    public void Accept(Collider enemy)
    {
        if (enemy.TryGetComponent(out IHealth health))
            health.TakeDamage(GetAdditionalDamage());
    }

    private float GetAdditionalDamage() => _data.Damage / 100 * AdditionalPercentageDamage;
}