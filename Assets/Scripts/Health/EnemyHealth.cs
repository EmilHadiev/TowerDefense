using System;
using UnityEngine;

[RequireComponent(typeof(TriggerObserver))]
public class EnemyHealth : MonoBehaviour, IHealth
{
    [SerializeField] private float _health;

    public float MaxHealth { get; private set; }

    public event Action Died;

    private void Start() => MaxHealth = _health;

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