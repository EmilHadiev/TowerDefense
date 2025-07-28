using UnityEngine;

public class EnemySpawnerAbility
{
    private const EnemyType Type = EnemyType.Skeleton;

    private readonly int _maxEnemies;

    private readonly IEnemyFactory _factory;
    private readonly EnemySpawnPosition[] _spanwer;
    private readonly IPool<Enemy> _pool;

    private int _previousSpawnPositionIndex = -1;

    public EnemySpawnerAbility(EnemySpawnPosition[] spawnPositions, IEnemyFactory factory, int maxEnemies, Optimizator fPSCounter)
    {
        _spanwer = spawnPositions;
        _maxEnemies = maxEnemies;
        _factory = factory;
        _pool = new EnemyPool(fPSCounter,_maxEnemies);
    }

    public void CreateEnemies()
    {
        for (int i = 0; i < _maxEnemies; i++)
        {
            Enemy enemy = _factory.Create(Type);
            enemy.gameObject.SetActive(false);
            _pool.Add(enemy);
        }
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
        int spawnPositionIndex = GetSpawnPositionIndex();

        enemy.transform.position = _spanwer[spawnPositionIndex].transform.position;
        enemy.transform.rotation = _spanwer[spawnPositionIndex].transform.rotation;
    }

    private int GetSpawnPositionIndex()
    {
        int spawnPositionIndex = Random.Range(0, _spanwer.Length);

        while (_previousSpawnPositionIndex == spawnPositionIndex)
            spawnPositionIndex = Random.Range(0, _spanwer.Length);

        _previousSpawnPositionIndex = spawnPositionIndex;
        return _previousSpawnPositionIndex;
    }
}
