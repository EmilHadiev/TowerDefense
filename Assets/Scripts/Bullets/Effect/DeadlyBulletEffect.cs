using UnityEngine;

public class DeadlyBulletEffect : IBulletEffectHandler
{
    private const float ChanceToKill = 30;

    public void HandleEffect(Collider enemy)
    {
        if (enemy.TryGetComponent(out EnemyHealth health))
            health.TakeDamage(health.MaxHealth);
    }
}