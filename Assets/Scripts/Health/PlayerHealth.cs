using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IHealth
{
    [SerializeField] private float _health;

    public event Action<float, float> HealthChanged;

    private float _maxHealth;

    private void Awake()
    {
        _maxHealth = _health;
    }

    private void OnEnable() => HealthChanged?.Invoke(_health, _maxHealth);

    public void AddHealth(float healthPoints)
    {
        throw new System.NotImplementedException();
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;

        HealthChanged?.Invoke(_health, _maxHealth);

        if (_health <= 0)
            gameObject.SetActive(false);
    }
}