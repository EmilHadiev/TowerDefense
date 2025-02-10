using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(TriggerObserver))]
public class EnemyHealth : MonoBehaviour, IHealth
{
    private float _health;
    private EnemyStat _stat;

    public float MaxHealth { get; private set; }

    public event Action Died;
    public event Action<float, float> HealthChanged;

    private void Awake()
    {
        _stat = GetComponent<Enemy>().Stat;
        _health = _stat.Health;
        MaxHealth = _health;
    }

    private void OnEnable() => HealthChanged?.Invoke(_health, MaxHealth);

    public void AddHealth(float healthPoints)
    {
        _health += healthPoints;
        HealthChanged?.Invoke(_health, MaxHealth);

        if (_health >= MaxHealth)
            _health = MaxHealth;
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
        HealthChanged?.Invoke(_health, MaxHealth);

        if (_health <= 0)
            Die();
    }

    private void Die()
    {
        _health = 0;
        Died?.Invoke();
    }
}