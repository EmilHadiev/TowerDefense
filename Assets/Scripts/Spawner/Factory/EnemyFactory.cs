using System;
using Zenject;

public class EnemyFactory : IEnemyFactory
{
    private IInstantiator _instantiator;

    public EnemyFactory(IInstantiator instantiator)
    {
        _instantiator = instantiator;
    }

    public Enemy Get(EnemyType enemyType)
    {
        switch (enemyType)
        {
            case EnemyType.Skeleton:
                return GetEnemy(AssetPath.SkeletonPath);
            case EnemyType.DemonKnight:
                return GetEnemy(AssetPath.DemonKnightPath);
            case EnemyType.Golem:
                return GetEnemy(AssetPath.GolemPath);
            case EnemyType.BlackKnight:
                return GetEnemy(AssetPath.BlackKnightPath);
            case EnemyType.ArmorKnight:
                return GetEnemy(AssetPath.ArmorKnightPath);
            case EnemyType.Turtle:
                return GetEnemy(AssetPath.TurtlePath);
            case EnemyType.Dragon:
                return GetEnemy(AssetPath.DragonPath);
            default:
                throw new ArgumentOutOfRangeException(nameof(enemyType));
        }
    }

    private Enemy GetEnemy(string path) => _instantiator.InstantiatePrefabResourceForComponent<Enemy>(path);
}