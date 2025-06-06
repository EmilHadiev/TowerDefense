﻿using System.Collections.Generic;

public class BulletEffectSetter
{
    private readonly List<Bullet> _bullets;

    public BulletEffectSetter()
    {
        _bullets = new List<Bullet>(15);
    }

    public void AddBullet(Bullet bullet)
    {
        _bullets.Add(bullet);
        SetBulletEffect(bullet.Type);
    }

    public void SetBulletEffect(BulletType type)
    {
        switch (type)
        {
            case BulletType.Fireball:
                SetEffectHandler<ExtraDamageBulletEffect>();
                break;
            case BulletType.Electric:
                SetEffectHandler<SplashBulletEffect>();
                break;
            case BulletType.Ice:
                SetEffectHandler<SlowdownBulletEffect>();
                break;
            case BulletType.Pushing:
                SetEffectHandler<PushingBulletEffect>();
                break;
            case BulletType.Deadly:
                SetEffectHandler<DeadlyBulletEffect>();
                break;
            case BulletType.Poison:
                SetEffectHandler<PoisonBulletEffect>();
                break;
            case BulletType.Blood:
                SetEffectHandler<VampirismEffect>();
                break;
            case BulletType.Golden:
                SetEffectHandler<GoldenBulletEffect>();
                break;
            case BulletType.Chaos:
                SetEffectHandler<RandomBulletEffect>();
                break;
            default:
                SetEffectHandler<EmptyBulletEffect>();
                break;
        }
    }

    private void SetEffectHandler<T>() where T : IBulletEffectHandler
    {
        for (int i = 0; i < _bullets.Count; i++)
            _bullets[i].SetEffect<T>();
    }
}