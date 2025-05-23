using UnityEngine;

public interface IFactoryParticle
{
    ParticleView Create(string path, Transform parent = null);
}
