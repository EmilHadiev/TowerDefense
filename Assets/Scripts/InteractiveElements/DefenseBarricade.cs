using System.Collections;
using UnityEngine;

public class DefenseBarricade : Barricade
{
    [SerializeField] private ObstacleHealth _health;

    private Coroutine _healthCoroutine;

    private readonly WaitForSeconds _delay = new WaitForSeconds(1);

    private void OnValidate()
    {
        _health ??= GetComponent<ObstacleHealth>();
    }

    private void OnEnable()
    {
        _healthCoroutine = StartCoroutine(HealthCoroutine());
    }

    private void OnDisable()
    {
        StopCoroutine(_healthCoroutine);
    }

    private IEnumerator HealthCoroutine()
    {
        while (true)
        {
            yield return _delay;
            _health.AddHealth(GetHealthPoints());
            Debug.Log("»сцул€ю на " + GetHealthPoints());
        }
    }

    private float GetHealthPoints()
    {
        float healthPercent = 0.9f;
        
        return _health.MaxHealth - _health.MaxHealth * healthPercent;
    }
}