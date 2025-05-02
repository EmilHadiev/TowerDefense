using System;
using UnityEngine;

[RequireComponent(typeof(TriggerObserver))]
public class EnemyHealth : MonoBehaviour, IHealth, IDamagable
{
    private float _health;
    private EnemyStat _stat;

    public float MaxHealth { get; private set; }

    public bool IsAlive => _health > 0;

    public event Action Died;
    public event Action<float> DamageApplied;
    public event Action<float, float> HealthChanged;

    private void Awake()
    {
        _stat = GetComponent<Enemy>().Stat;
        InitValue();
    }

    private void OnEnable()
    {
        InitValue();
        HealthChanged?.Invoke(_health, MaxHealth);
    }

    public void AddHealth(float healthPoints)
    {
        _health += healthPoints;
        HealthChanged?.Invoke(_health, MaxHealth);

        if (_health >= MaxHealth)
            _health = MaxHealth;
    }

    public void TakeDamage(float damage)
    {
        damage = (float)Math.Round(damage, 2);
        _health -= damage;

        HealthChanged?.Invoke(_health, MaxHealth);
        DamageApplied?.Invoke(damage);

        if (_health <= 0)
            Die();
    }

    private void Die()
    {
        _health = 0;
        Died?.Invoke();
    }

    private void InitValue()
    {
        _health = _stat.Health;
        MaxHealth = _health;
    }
}