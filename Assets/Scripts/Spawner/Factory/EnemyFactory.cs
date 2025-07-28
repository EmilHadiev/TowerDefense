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
            {EnemyType.Skeleton,    AssetProvider.SkeletonPath},
            {EnemyType.DemonKnight, AssetProvider.DemonKnightPath},
            {EnemyType.Golem,       AssetProvider.GolemPath},
            {EnemyType.BlackKnight, AssetProvider.BlackKnightPath},
            {EnemyType.ArmorKnight, AssetProvider.ArmorKnightPath},
            {EnemyType.Turtle,      AssetProvider.TurtlePath },
            {EnemyType.Dragon,      AssetProvider.DragonPath},
            {EnemyType.Slime,       AssetProvider.SlimePath},
            {EnemyType.Mage,        AssetProvider.MagePath},
        };

        if (_enemyPaths.Count != Enum.GetValues(typeof(EnemyType)).Length)
            throw new ArgumentOutOfRangeException(nameof(_enemyPaths));
    }

    public Enemy Create(EnemyType enemyType)
    {
        if (_enemyPaths.TryGetValue(enemyType, out string path))
            return GetEnemy(path);

        throw new ArgumentOutOfRangeException(nameof(enemyType));
    }

    private Enemy GetEnemy(string path) => _instantiator.InstantiatePrefabResourceForComponent<Enemy>(path);
}