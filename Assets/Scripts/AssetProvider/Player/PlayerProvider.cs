using UnityEngine;
using Zenject;

public class PlayerProvider : IPlayerProvider
{
    private readonly IInstantiator _instantiator;
    private Player _player;

    public PlayerProvider(IInstantiator instantiator)
    {
        _instantiator = instantiator;
    }

    public Player Create(string path)
    {
        _player = _instantiator.InstantiatePrefabResourceForComponent<Player>(path);
        return _player;
    }

    public void SetPosition(Vector3 position)
    {
        _player.Transform.position = position;
    }
}
