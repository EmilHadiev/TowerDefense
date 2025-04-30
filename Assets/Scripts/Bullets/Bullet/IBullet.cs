using System;
using System.Collections.Generic;
using UnityEngine;

public interface IBullet
{
    Color Color { get; }
    BulletData Data { get; }
    BulletType Type { get; }

    void InitBullet(PlayerStat stat, IReadOnlyDictionary<Type, IBulletEffectHandler> bulletEffects);
    void SetEffect<T>() where T : IBulletEffectHandler;
}