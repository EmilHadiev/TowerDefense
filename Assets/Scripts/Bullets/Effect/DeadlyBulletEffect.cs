﻿using UnityEngine;

public class DeadlyBulletEffect : IBulletEffectHandler
{
    private const int ChanceToKill = 15;

    public void HandleEffect(Collider enemy)
    {
        if (enemy.TryGetComponent(out EnemyHealth health))
            TryToKill(health);
    }

    private void TryToKill(EnemyHealth health)
    {
        int rand = Random.Range(0, 100);

        if (rand <= ChanceToKill)
            health.TakeDamage(health.MaxHealth);
    }
}