using System;

public interface IHealth
{
    void AddHealth(float healthPoints);
    void TakeDamage(float damage);
}