using System;
using UnityEngine;

public class ParticleViewContainer : MonoBehaviour, IParticleViewContainer
{
    [SerializeField] private ParticleView _particleView;

    private IHealth _health;
    private Color _currentColor;

    private void OnValidate()
    {
        if (_particleView == null)
            throw new ArgumentNullException(nameof(_particleView));
    }

    private void Awake() => _health = GetComponent<IHealth>();

    private void Start() => SetDamageParticleColor(Color.yellow);

    public void SetDamageParticleColor(Color color)
    {
        if (_currentColor == color)
            return;

        _currentColor = color;
        _particleView.SetColor(color);
    }

    private void OnEnable()
    {
        _health.HealthChanged += OnHealthChanged;
        _particleView.Stop();
    }


    private void OnDisable() => _health.HealthChanged += OnHealthChanged;

    private void OnHealthChanged(float currentHealth, float maxHealth) => _particleView.Play();
}