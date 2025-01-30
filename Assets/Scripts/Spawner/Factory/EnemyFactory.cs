using System;
using Zenject;

public class EnemyFactory : IEnemyFactory
{
    private IInstantiator _instantiator;

    public EnemyFactory(IInstantiator instantiator)
    {
        _instantiator = instantiator;
    }

    public Enemy Get(EnemyType type)
    {
        switch (type)
        {
            case EnemyType.Skeleton:
                return _instantiator.InstantiatePrefabResourceForComponent<Enemy>(AssetPath.SkeletonPath);
            default:
                throw new ArgumentOutOfRangeException(nameof(type));
        }
    }
}