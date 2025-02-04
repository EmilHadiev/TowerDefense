using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasRotator))]
public class EnemyHealthView : MonoBehaviour
{
    [SerializeField] private Image _fillImage;

    private IHealth _health;

    private void Awake() => _health = GetComponentInParent<IHealth>();

    private void OnEnable() => _health.HealthChanged += OnHealthChanged;

    private void OnDisable() => _health.HealthChanged -= OnHealthChanged;

    private void OnHealthChanged(float currentHealth, float maxHealth) =>
        _fillImage.fillAmount = currentHealth / maxHealth;
}