using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(TriggerObserver))]
public class EnemyHealth : MonoBehaviour, IHealth
{
    private SkeletonStat _stat;
    public float MaxHealth { get; private set; }
    private float _health;

    public event Action Died;
    public event Action<float, float> HealthChanged;

    private void Awake()
    {
        _health = _stat.Health;
        MaxHealth = _health;
    }

    private void OnEnable() => HealthChanged?.Invoke(_health, MaxHealth);

    [Inject]
    private void Constructor(SkeletonStat stat)
    {
        _stat = stat;
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
        _health -= damage;
        HealthChanged?.Invoke(_health, MaxHealth);

        if (_health <= 0)
            Died?.Invoke();
    }
}