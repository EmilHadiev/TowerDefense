using System;
using UnityEngine;

public class ParticleViewContainer : MonoBehaviour
{
    [SerializeField] private ParticleView _particleView;

    private IHealth _health;

    private void OnValidate()
    {
        if (_particleView == null)
            throw new ArgumentNullException(nameof(_particleView));
    }

    private void Awake()
    {
        _health = GetComponent<IHealth>();
    }

    private void Start()
    {
        _particleView.SetColor(Color.yellow);
    }

    private void OnEnable()
    {
        _health.HealthChanged += OnHealthChanged;
        _particleView.Stop();
    }
    

    private void OnDisable()
    {
        _health.HealthChanged += OnHealthChanged;
    }

    private void OnHealthChanged(float currentHealth, float maxHealth)
    {
        _particleView.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Bullet bullet))
        {
            Debug.Log("Проверка!");
        }
    }
}