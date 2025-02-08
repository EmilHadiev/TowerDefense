using UnityEngine;
using Zenject;

public class EnemySpawnerAbilityView
{
    private readonly IInstantiator _instantiator;
    private readonly Transform _spawner;
    private readonly ParticleView _particleView;

    public EnemySpawnerAbilityView(EnemySpawnPosition spawner, IInstantiator instantiator)
    {
        _spawner = spawner.transform;
        _instantiator = instantiator;
        _particleView = CreateParticle();
    }

    private ParticleView CreateParticle()
    {
        ParticleView view = _instantiator.InstantiatePrefabResourceForComponent<ParticleView>(AssetPath.ParticleSpawnRunePath);
        view.transform.parent = _spawner.transform;
        view.Stop();
        return view;
    }

    public void Show()
    {
        _particleView.transform.position = _spawner.transform.position;
        _particleView.transform.rotation = _spawner.transform.rotation;
        _particleView.Play();
    }

    public void Stop() => _particleView.Stop();
}