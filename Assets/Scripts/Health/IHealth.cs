using System;

public interface IHealth
{
    event Action<float, float> HealthChanged;
    event Action Died;
    event Action<float> DamageApplied;

    bool IsAlive { get; }

    void AddHealth(float healthPoints);
    void TakeDamage(float damage);
}