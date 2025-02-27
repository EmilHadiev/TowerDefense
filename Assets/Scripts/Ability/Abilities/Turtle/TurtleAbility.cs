using UnityEngine;
using System;
using Zenject;

public class TurtleAbility : MonoBehaviour
{
    private IHealth _health;
    private EnemyExplosion _ability;
    private IInstantiator _instantiator;

    private ParticleView _explosionPrefab;
    private ParticleViewText _explosionCountPrefab;
    private EnemyStat _stat;

    private void Awake()
    {
        _health = GetComponent<IHealth>();
        _stat = GetComponent<Enemy>().Stat;


        if (_health == null)
            throw new ArgumentNullException(nameof(_health));
    }

    private void OnEnable() => _health.Died += OnDied;

    private void OnDisable() => _health.Died -= OnDied;

    private void Start()
    {
        CreateExplosionParticle();
        CreateExplosionCountParticle();
        _ability = new EnemyExplosion(transform, _stat, _explosionCountPrefab);
    }

    [Inject]
    private void Constructor(IInstantiator instantiator) => _instantiator = instantiator;

    private void CreateExplosionParticle()
    {
        _explosionPrefab = _instantiator.InstantiatePrefabResourceForComponent<ParticleView>(AssetPath.ParticleExplosionPath);
        _explosionPrefab.Stop();
    }

    private void CreateExplosionCountParticle()
    {
        _explosionCountPrefab = _instantiator.InstantiatePrefabResourceForComponent<ParticleViewText>(AssetPath.ParticleExplosionCountPath);
        _explosionCountPrefab.Stop();
    }

    private void OnDied()
    {        
        ExplodeView();
        _ability.Activate();
        ExplodeCountView();
    }

    private void ExplodeView()
    {
        _explosionPrefab.transform.position = transform.position;
        _explosionPrefab.transform.rotation = transform.rotation;
        _explosionPrefab.Play();
    }

    private void ExplodeCountView()
    {
        _explosionCountPrefab.transform.position = GetExplosionCountPosition();
        _explosionCountPrefab.transform.rotation = transform.rotation;
        _explosionCountPrefab.Play();
    }

    private Vector3 GetExplosionCountPosition()
    {
        Vector3 additionalPosition = transform.up * 2;
        return transform.position + additionalPosition;
    }
}