using System;

public interface IHealth
{
    event Action<float, float> HealthChanged;
    event Action Died;

    bool IsAlive { get; }

    void AddHealth(float healthPoints);
    void TakeDamage(float damage);
}