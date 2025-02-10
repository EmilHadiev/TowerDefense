using UnityEngine;

public class FireballBullet : IBulletPolicy
{
    private const float AdditionalPercentageDamage = 300;

    public void Accept(Collider enemy)
    {
        if (enemy.TryGetComponent(out IHealth health))
            health.TakeDamage(300);
    }
}