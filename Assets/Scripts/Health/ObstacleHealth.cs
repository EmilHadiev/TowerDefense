using System;
using UnityEngine;
using Zenject;

public class ObstacleHealth : MonoBehaviour, IHealth
{
    private PlayerStat _playerStat;

    private float _health;

    public float MaxHealth { get; private set; }

    public bool IsAlive => _health > 0;

    public event Action<float, float> HealthChanged;
    public event Action Died;
    public event Action<float> DamageApplied;

    [Inject]
    private void Constructor(PlayerStat playerStat)
    {
        _playerStat = playerStat;
    }

    private void OnEnable()
    {
        MaxHealth = _playerStat.MaxHealth;
        _health = MaxHealth;
    }

    public void AddHealth(float healthPoints)
    {
        _health += healthPoints;

        if (_health > MaxHealth)
            _health = MaxHealth;

        
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;

        if (_health <= 0)
            Die();

        DamageApplied?.Invoke(damage);
    }

    private void Die()
    {
        gameObject.SetActive(false);
    }
}