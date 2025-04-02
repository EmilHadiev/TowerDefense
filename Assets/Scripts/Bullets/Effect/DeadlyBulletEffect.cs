using UnityEngine;

public class DeadlyBulletEffect : IBulletEffectHandler
{
    private const int ChanceToKill = 30;

    public void HandleEffect(Collider enemy)
    {
        if (enemy.TryGetComponent(out EnemyHealth health))
            if (TryToKill())
                health.TakeDamage(health.MaxHealth);
    }

    private bool TryToKill()
    {
        int rand = Random.Range(0, 100);

        if (rand <= ChanceToKill)
            return true;

        return false;
    }
}