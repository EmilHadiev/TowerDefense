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

    private void Start()
    {
        _health = _stat.Health;
        MaxHealth = _health;
    }

    [Inject]
    private void Constructor(SkeletonStat stat)
    {
        _stat = stat;
    }

    public void AddHealth(float healthPoints)
    {
        _health += healthPoints;

        if (_health >= MaxHealth)
            _health = MaxHealth;
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;

        if (_health <= 0)
            Died?.Invoke();
    }
}