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

    private void Awake()
    {
        _health = _stat.Health;
        _maxHealth = _health;
    }

    private void OnEnable() => HealthChanged?.Invoke(_health, _maxHealth);

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

        if (_health <= 0)
            Die();
    }

    private void Die()
    {
        Died?.Invoke();
        gameObject.SetActive(false);
    }
}