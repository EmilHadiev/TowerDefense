using System;
using UnityEngine;
using Zenject;

public class EnemySpawnerAbility
{
    private const EnemyType ProhibitedType = EnemyType.Mage;

    private readonly IEnemyFactory _factory;
    private readonly Transform _spanwer;
    private readonly IPool<Enemy> _pool;
    private readonly EnemyType _type;

    public EnemySpawnerAbility(EnemySpawnPosition spawner, IInstantiator instantiator)
    {
        _factory = new EnemyFactory(instantiator);
        _spanwer = spawner.transform;
        _pool = new EnemyPool(1);
        
        _type = GetRandomEnemyType();
        CreateEnemy();
    }

    private void CreateEnemy()
    {
        Enemy enemy = _factory.Get(_type);
        enemy.gameObject.SetActive(false);
        _pool.Add(enemy);
    }

    private EnemyType GetRandomEnemyType()
    {
        EnemyType[] enemyTypes = (EnemyType[])Enum.GetValues(typeof(EnemyType));
        int randomIndex = UnityEngine.Random.Range(0, enemyTypes.Length);

        while (enemyTypes[randomIndex] == ProhibitedType)
            randomIndex = UnityEngine.Random.Range(0, enemyTypes.Length);

        return enemyTypes[randomIndex];
    }

    public bool TrySpawn()
    {
        if (_pool.TryGet(out Enemy enemy))
        {
            SetPosition(enemy);
            enemy.gameObject.SetActive(true);
            return true;
        }

        return false;
    }

    private void SetPosition(Enemy enemy)
    {
        enemy.transform.position = _spanwer.transform.position;
        enemy.transform.rotation = _spanwer.transform.rotation;
    }
}
