using System;
using UnityEngine;

public class ParticleViewContainer : MonoBehaviour, IParticleColorChangable
{
    [SerializeField] private ParticleView _damageImpact;
    [SerializeField] private ParticleViewText _damageValue;

    private IHealth _health;
    private Color _currentColor;

    private void OnValidate()
    {
        if (_damageImpact == null)
            throw new ArgumentNullException(nameof(_damageImpact));

        if (_damageValue == null)
            throw new ArgumentNullException(nameof(_damageValue));
    }

    private void Awake() => _health = GetComponent<IHealth>();

    private void OnEnable()
    {
        _health.HealthChanged += OnHealthChanged;
        _health.DamageApplied += OnDamageApplied;

        _damageValue.Stop();
        _damageImpact.Stop();
    }

    private void OnDisable()
    {
        _health.HealthChanged -= OnHealthChanged;
        _health.DamageApplied -= OnDamageApplied;
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

    private void OnHealthChanged(float currentHealth, float maxHealth) => PlayParticle(_damageImpact);

    private void OnDamageApplied(float damage)
    {
        if (damage == 0)
            return;

        damage = (float)Math.Round(damage, 2);
        _damageValue.SetText(damage.ToString());
        PlayParticle(_damageValue);
    }

    private void PlayParticle(ParticleView particle)
    {
        particle.Stop();
        particle.Play();
    }
}