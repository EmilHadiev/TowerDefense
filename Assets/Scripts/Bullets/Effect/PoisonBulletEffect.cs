using UnityEngine;

public class PoisonBulletEffect : IBulletEffectHandler
{
    public void HandleEffect(Collider enemy)
    {
        if (enemy.TryGetComponent(out INegativeEffectContainer container))
            container.Activate<PoisonEffect>();
    }
}