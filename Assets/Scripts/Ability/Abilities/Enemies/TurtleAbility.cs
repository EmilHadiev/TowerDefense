using UnityEngine;
using System;

public class TurtleAbility : MonoBehaviour
{
    private IHealth _health;
    private IAbility _ability;

    private void Awake()
    {
        _health = GetComponent<IHealth>();

        if (_health == null)
            throw new ArgumentNullException(nameof(_health));
    }

    private void OnEnable() => _health.Died += OnDied;

    private void OnDisable() => _health.Died -= OnDied;

    private void Start() => _ability = new EnemyExplosion(transform);

    private void OnDied() => _ability.Activate();
}