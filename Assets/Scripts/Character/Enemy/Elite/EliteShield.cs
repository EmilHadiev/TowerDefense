using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(ReflectiveObstacle))]
public class EliteShield : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private EliteShieldHealthView _view;

    private ShieldHealth _shieldHealth;
    private IEnemySoundContainer _soundContainer;

    private void Awake()
    {
        _shieldHealth = new ShieldHealth(_maxHealth);
    }

    private void OnEnable()
    {
        _shieldHealth.Died += OnShielDestroy;
        _shieldHealth.HealthChanged += OnShieldHealthChanged;
        _shieldHealth.ResetHealth();
    }

    private void OnDisable()
    {
        _shieldHealth.Died -= OnShielDestroy;
        _shieldHealth.HealthChanged -= OnShieldHealthChanged;
    }

    [Inject]
    private void Constructor(IEnemySoundContainer soundContainer)
    {
        _soundContainer = soundContainer;
    }

    private void OnShieldHealthChanged(int health)
    {
        _view.SetHealth(health);
        _soundContainer.Play(SoundName.ShieldDamageImpact);
    }

    private void OnShielDestroy()
    {
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        _shieldHealth.TakeDamage();
    }

    private class ShieldHealth
    {
        private const int Damage = 1;

        private readonly int _maxHealth;
        
        public int Health { get; private set; }

        public Action<int> HealthChanged;
        public Action Died;

        public ShieldHealth(int maxHealth)
        {
            _maxHealth = maxHealth;
            ResetHealth();
        }

        public void ResetHealth()
        {
            Health = _maxHealth;
            HealthChanged?.Invoke(Health);
        }

        public void TakeDamage()
        {
            Health -= Damage;
            HealthChanged?.Invoke(Health);

            if (Health <= 0)
                Died?.Invoke();
        }
    }
}