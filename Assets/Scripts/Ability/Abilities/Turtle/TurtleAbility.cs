using UnityEngine;
using System;
using Zenject;

public class TurtleAbility : MonoBehaviour
{
    private IHealth _health;
    private IAbility _ability;
    private IInstantiator _instantiator;

    private ParticleView _view;

    private void Awake()
    {
        _health = GetComponent<IHealth>();

        if (_health == null)
            throw new ArgumentNullException(nameof(_health));
    }

    private void OnEnable() => _health.Died += OnDied;

    private void OnDisable() => _health.Died -= OnDied;

    private void Start()
    {
        _ability = new EnemyExplosion(transform);
        CreateExplosionParticle();
    }

    [Inject]
    private void Constructor(IInstantiator instantiator) => _instantiator = instantiator;

    private void CreateExplosionParticle()
    {
        _view = _instantiator.InstantiatePrefabResourceForComponent<ParticleView>(AssetPath.ParticleExplosionPath);
        _view.Stop();
    }

    private void OnDied()
    {        
        Explode();
        _ability.Activate();
    }

    private void Explode()
    {
        _view.transform.position = transform.position;
        _view.transform.rotation = transform.rotation;
        _view.Play();
    }
}