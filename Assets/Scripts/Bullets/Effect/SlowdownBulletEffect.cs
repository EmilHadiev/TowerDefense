using UnityEngine;

public class SlowdownBulletEffect : IBulletEffectHandler
{
    public void HandleEffect(Collider enemy)
    {
        if (enemy.TryGetComponent(out INegativeEffectContainer container))
            container.Activate<FreezingEffect>();
    }
}