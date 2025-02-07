using System;

public interface IHealth
{
    event Action<float, float> HealthChanged;
    event Action Died;
    void AddHealth(float healthPoints);
    void TakeDamage(float damage);
}