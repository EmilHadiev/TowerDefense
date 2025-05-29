using UnityEngine;
using Zenject;

public class InteractiveElementFactory
{
    private readonly IInstantiator _instantiator;
    private readonly IPlayer _player;

    public InteractiveElementFactory(IInstantiator instantiator, IPlayer player)
    {
        _instantiator = instantiator;
        _player = player;
    }

    public GameObject Create(InteractiveElement interactiveElement) =>
        _instantiator.InstantiatePrefab(interactiveElement, _player.Transform.position, _player.Transform.rotation, null);
}