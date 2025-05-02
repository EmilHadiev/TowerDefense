using System;

public interface IDamagable
{
    event Action<float> DamageApplied;
}