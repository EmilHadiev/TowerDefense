using System;
using System.Collections.Generic;
using Zenject;

public class EnemyFactory : IEnemyFactory
{
    private readonly IInstantiator _instantiator;
    private readonly Dictionary<EnemyType, string> _enemyPaths;

    public EnemyFactory(IInstantiator instantiator)
    {
        _instantiator = instantiator;

        _enemyPaths = new Dictionary<EnemyType, string>(10)
        {
            {EnemyType.Skeleton,    AssetPath.SkeletonPath},
            {EnemyType.DemonKnight, AssetPath.DemonKnightPath},
            {EnemyType.Golem,       AssetPath.GolemPath},
            {EnemyType.BlackKnight, AssetPath.BlackKnightPath},
            {EnemyType.ArmorKnight, AssetPath.ArmorKnightPath},
            {EnemyType.Turtle,      AssetPath.TurtlePath },
            {EnemyType.Dragon,      AssetPath.DragonPath},
            {EnemyType.Slime,       AssetPath.SlimePath},
            {EnemyType.Mage,        AssetPath.MagePath},
        };

        if (_enemyPaths.Count != Enum.GetValues(typeof(EnemyType)).Length)
            throw new ArgumentOutOfRangeException(nameof(_enemyPaths));
    }

    public Enemy Get(EnemyType enemyType)
    {
        if (_enemyPaths.TryGetValue(enemyType, out string path))
            return GetEnemy(path);

        throw new ArgumentOutOfRangeException(nameof(enemyType));
    }

    private Enemy GetEnemy(string path) => _instantiator.InstantiatePrefabResourceForComponent<Enemy>(path);
}