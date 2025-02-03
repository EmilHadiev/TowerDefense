using System;

public interface IHealth
{
    event Action<float, float> HealthChanged;
    void AddHealth(float healthPoints);
    void TakeDamage(float damage);
}