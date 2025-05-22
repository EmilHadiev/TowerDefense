using System;
using UnityEngine;
using Zenject;

public class PlayerHealth : MonoBehaviour, IHealth
{
    private PlayerStat _stat;
    private IResurrectable _resurrectable;

    private float _maxHealth;
    private float _health;

    private bool _isDead;

    public bool IsAlive => _health > 0;

    public event Action<float, float> HealthChanged;
    public event Action Died;

    private void Awake()
    {
        _maxHealth = _stat.MaxHealth;
        _health = _maxHealth;
        _resurrectable = GetComponent<IResurrectable>();
    }

    private void OnEnable()
    {
        _stat.HealthProperty.Changed += OnHealthUpgraded;
        HealthChanged?.Invoke(_health, _maxHealth);
        _resurrectable.Resurrected += OnResurrected;
    }

    private void OnDisable()
    {
        _stat.HealthProperty.Changed -= OnHealthUpgraded;
        _resurrectable.Resurrected -= OnResurrected;
    }

    [Inject]
    private void Constructor(PlayerStat playerStat)
    {
        _stat = playerStat;
    }

    public void AddHealth(float healthPoints)
    {
        _health += healthPoints;
        HealthChanged?.Invoke(_health, _maxHealth);

        if (_health >= _maxHealth)
            _health = _maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (_isDead)
            return;

        _health -= damage;

        HealthChanged?.Invoke(_health, _maxHealth);

        if (_health <= 0)
            Die();
    }

    private void Die()
    {
        _isDead = true;
        Died?.Invoke();
    }

    private void OnHealthUpgraded(float health)
    {
        _health = health;
        _maxHealth = _health;

        HealthChanged?.Invoke(_health, _maxHealth);
    }

    private void OnResurrected() => _isDead = false;
}