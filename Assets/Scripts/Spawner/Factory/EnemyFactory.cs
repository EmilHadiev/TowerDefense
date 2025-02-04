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
                return GetEnemy(AssetPath.SkeletonPath);
            case EnemyType.DemonKnight:
                return GetEnemy(AssetPath.DemonKnightPath);
            case EnemyType.Golem:
                return GetEnemy(AssetPath.GolemPath);
            case EnemyType.BlackKnight:
                return GetEnemy(AssetPath.BlackKnightPath);
            default:
                throw new ArgumentOutOfRangeException(nameof(type));
        }
    }

    private Enemy GetEnemy(string path) => _instantiator.InstantiatePrefabResourceForComponent<Enemy>(path);
}