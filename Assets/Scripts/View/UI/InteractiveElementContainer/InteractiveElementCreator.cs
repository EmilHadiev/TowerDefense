using System.Collections.Generic;
using UnityEngine;

public class InteractiveElementCreator
{
    private readonly IFactoryParticle _factoryParticle;
    private readonly ParticleView _particleView;
    private readonly Queue<InteractiveElement> _elements = new Queue<InteractiveElement>(15);
    private readonly ISoundContainer _soundContainer;
    private readonly InteractiveElementPool _pools;
    private readonly ICoinStorage _coinStorage;

    public InteractiveElementCreator(IFactoryParticle factoryParticle, InteractiveElementPool pools, ISoundContainer soundContainer, ICoinStorage coinStorage    )
    {
        _factoryParticle = factoryParticle;
        _soundContainer = soundContainer;
        _pools = pools;
        _coinStorage = coinStorage;

        _particleView = CreateParticle();
        _particleView.Stop();        
    }

    private ParticleView CreateParticle() =>
        _factoryParticle.Create(AssetProvider.ParticleBuildPath);

    public void SetElement(Vector3 position)
    {
        if (_elements.Count == 0)
            return;

        PlayParticleView(position);
        CreateInteractiveElement(position);
    }

    public bool TryPurchaseElement(InteractiveElementData currentData)
    {
        if (currentData == null)
            return false;

        if (_coinStorage.TrySpend(currentData.Price))
        {
            InteractiveElement element = _pools.Get(currentData.Prefab);
            element.IsPurchased = true;
            _elements.Enqueue(element);

            _soundContainer.Play(SoundName.SpendCoin);
            return true;
        }

        return false;
    }

    private void PlayParticleView(Vector3 position)
    {
        _particleView.transform.position = position;
        _particleView.Play();
    }

    private void CreateInteractiveElement(Vector3 position)
    {
        var prefab = _elements.Dequeue();
        prefab.transform.position = position;

        prefab.IsPurchased = false;
        prefab.gameObject.SetActive(true);
        _soundContainer.Play(SoundName.Building);
    }
}