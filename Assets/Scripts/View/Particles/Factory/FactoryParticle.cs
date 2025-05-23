using UnityEngine;
using Zenject;

public class FactoryParticle : IFactoryParticle
{
    private readonly IInstantiator _instantiator;

    public FactoryParticle(IInstantiator instantiator)
    {
        _instantiator = instantiator;
    }

    public ParticleView Create(string path, Transform parent = null) =>
        _instantiator.InstantiatePrefabResourceForComponent<ParticleView>(path, parent);
}