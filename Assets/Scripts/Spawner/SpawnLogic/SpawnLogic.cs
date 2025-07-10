using System.Collections.Generic;
using UnityEngine;

public abstract class SpawnLogic
{
    protected readonly WaveData WaveData;
    protected readonly Dictionary<EnemyType, EnemySpawner> Spawners = new Dictionary<EnemyType, EnemySpawner>();
    protected readonly List<Transform> SpawnPositions = new List<Transform>(6);

    public SpawnLogic(WaveData waveData, IEnumerable<EnemySpawner> spawners)
    {
        WaveData = waveData;

        foreach (var spawn in spawners)
        {
            Spawners.Add(spawn.EnemyType, spawn);
            SpawnPositions.Add(spawn.transform);
        }
    }

    public abstract bool TrySpawn();
    public abstract void CalculateNextWave();

    protected Vector3 GetRandomSpawnPosition()
    {
        int rand = Random.Range(0, SpawnPositions.Count);
        return SpawnPositions[rand].position;
    }

    protected bool TrySpawn(int enemiesCount, EnemyType enemyType)
    {
        if (enemiesCount <= 0)
            return false;

        Vector3 position = GetRandomSpawnPosition();
        if (Spawners[enemyType].TrySpawn(position))
        {
            enemiesCount--;
            return true;
        }

        return false;
    }

    protected bool TrySpawnRandom(int enemiesCount, params EnemyType[] enemyType)
    {
        if (enemiesCount <= 0)
            return false;

        Vector3 position = GetRandomSpawnPosition();
        EnemyType randomEnemy = enemyType[Random.Range(0, enemyType.Length)];
        if (Spawners[randomEnemy].TrySpawn(position))
        {
            enemiesCount--;
            return true;
        }

        return false;
    }
}