using System;
using UnityEngine;

public class ObstacleHealth : MonoBehaviour, IHealth
{
    [SerializeField] private float _health = 100;

    public bool IsAlive => _health > 0;

    public event Action<float, float> HealthChanged;
    public event Action Died;
    public event Action<float> DamageApplied;

    public void AddHealth(float healthPoints)
    {
        throw new NotImplementedException();
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;

        if (_health <= 0)
            Die();
    }

    private void Die()
    {
        gameObject.SetActive(false);
    }
}