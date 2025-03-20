using System;
using UnityEngine;
using Zenject;

public class PlayerHealth : MonoBehaviour, IHealth
{
    private PlayerStat _stat;

    private float _maxHealth;
    private float _health;
    
    public bool IsAlive => _health > 0;

    public event Action<float, float> HealthChanged;
    public event Action Died;
    public event Action<float> DamageApplied;

    private void Awake()
    {
        _maxHealth = _stat.MaxHealth;
        _health = _maxHealth;
    }

    private void OnEnable()
    {
        _stat.HealthProperty.Changed += OnHealthUpgraded;
        HealthChanged?.Invoke(_health, _maxHealth);
    }

    private void OnDisable() => _stat.HealthProperty.Changed -= OnHealthUpgraded;

    [Inject]
    private void Constructor(PlayerStat playerStat)
    {
        _stat = playerStat;
    }

    public void AddHealth(float healthPoints)
    {
        throw new System.NotImplementedException();
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;

        HealthChanged?.Invoke(_health, _maxHealth);
        DamageApplied?.Invoke(damage);

        if (_health <= 0)
            Die();
    }

    private void Die()
    {
        Died?.Invoke();
        gameObject.SetActive(false);
    }

    private void OnHealthUpgraded(float health)
    {
        _health = health;
        _maxHealth = _health;

        HealthChanged?.Invoke(_health, _maxHealth);
    }
}