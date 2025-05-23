using UnityEngine;

public class EnemySpawnerAbilityView
{
    private readonly IFactoryParticle _factoryParticle;
    private readonly Transform _spawner;
    private readonly ParticleView _particleView;

    public EnemySpawnerAbilityView(EnemySpawnPosition spawner, IFactoryParticle factoryParticle)
    {
        _spawner = spawner.transform;
        _factoryParticle = factoryParticle;
        _particleView = CreateParticle();
    }

    private ParticleView CreateParticle()
    {
        ParticleView view = _factoryParticle.Create(AssetProvider.ParticleSpawnRunePath, _spawner);
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