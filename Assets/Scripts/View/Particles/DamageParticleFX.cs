using System;
using UnityEngine;

public class DamageParticleFX : MonoBehaviour, IParticleColorChangable
{
    [SerializeField] private ParticleView _damageImpact;
    [SerializeField] private ParticleViewText _damageValue;

    private IHealth _health;
    private IDamagable _damagable;
    private Color _currentColor;

    private void OnValidate()
    {
        if (_damageImpact == null)
            throw new ArgumentNullException(nameof(_damageImpact));

        if (_damageValue == null)
            throw new ArgumentNullException(nameof(_damageValue));
    }

    private void Awake()
    {
        _health = GetComponent<IHealth>();
        _damagable = GetComponent<IDamagable>();
    }

    private void OnEnable()
    {
        _health.HealthChanged += OnHealthChanged;
        _damagable.DamageApplied += OnDamageApplied;
        StopParticles();
    }

    private void OnDisable()
    {
        _health.HealthChanged -= OnHealthChanged;
        _damagable.DamageApplied -= OnDamageApplied;
    }

    private void Start() => SetDamageParticleColor(Color.yellow);

    public void SetDamageParticleColor(Color color)
    {
        if (_currentColor == color)
            return;

        _currentColor = color;
        SetParticlesColor(color);
    }

    private void SetParticlesColor(Color color)
    {
        _damageImpact.SetColor(color);
        _damageValue.SetColor(color);
    }

    private void OnHealthChanged(float currentHealth, float maxHealth) => _damageImpact.Play();

    private void OnDamageApplied(float damage)
    {
        if (damage == 0)
            return;

        PlayTextToParticle(_damageValue, damage);       
    }

    private void PlayTextToParticle(ParticleViewText particle, float damage)
    {
        damage = (float)Math.Round(damage, 2);
        particle.SetText(damage.ToString());
        particle.Play();
    }

    private void StopParticles()
    {
        _damageValue.Stop();
        _damageImpact.Stop();
    }
}