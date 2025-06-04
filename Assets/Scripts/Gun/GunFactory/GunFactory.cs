using Zenject;

public class GunFactory : IGunFactory
{
    private readonly IInstantiator _instantiator;

    public GunFactory(IInstantiator instantiator)
    {
        _instantiator = instantiator;
    }

    public Gun Create(Gun prefab)
    {
        Gun gun = _instantiator.InstantiatePrefab(prefab).GetComponent<Gun>();
        return gun;
    }
}