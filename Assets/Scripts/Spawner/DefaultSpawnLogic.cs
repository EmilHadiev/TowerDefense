using System;
using System.Collections.Generic;
using UnityEngine;

public class DefaultSpawnLogic : ISpawnLogic
{
    private readonly WaveData _waveData;
    private readonly Dictionary<EnemyType, EnemySpawner> _spawners = new Dictionary<EnemyType, EnemySpawner>();
    private readonly List<Transform> _positions = new List<Transform>(6);

    EnemyType[] _otherEnemies = { EnemyType.Dragon, EnemyType.BlackKnight, EnemyType.Mage, EnemyType.ArmorKnight };

    private int _skeletons;
    private int _mageAndKnightAndDragon;
    private int _elite;

    public DefaultSpawnLogic(WaveData waveData, IEnumerable<EnemySpawner> spawners)
    {
        _waveData = waveData;

        foreach (var spawn in spawners)
        {
            _spawners.Add(spawn.EnemyType, spawn);
            _positions.Add(spawn.transform);
        }
    }

    public void CalculateNextWave()
    {
        int maxEnemies = _waveData.MaxEnemies;
        int total = maxEnemies;

        _skeletons = Convert.ToInt32(maxEnemies / 2);
        int others = maxEnemies - _skeletons;

        if (_waveData.IsFinalWave)
        {
            _elite = 1;
            _mageAndKnightAndDragon = others - _elite;
        }
        else
        {
            _elite = 0;
            _mageAndKnightAndDragon = others;
        }

        total -= (_skeletons + _mageAndKnightAndDragon + _elite);
        _mageAndKnightAndDragon += total;
    }

    public bool TrySpawn()
    { 
        return TrySpawnSkeleton() || TrySpawnRandomOther() || TrySpawnElite();
    }

    private bool TrySpawnSkeleton()
    {
        if (_skeletons <= 0)
            return false;

        Vector3 position = GetRandomSpawnPosition();
        if (_spawners[EnemyType.Skeleton].TrySpawn(position))
        {
            _skeletons--;
            return true;
        }
        return false;
    }

    private bool TrySpawnRandomOther()
    {
        if (_mageAndKnightAndDragon <= 0)
            return false;

        EnemyType type = _otherEnemies[UnityEngine.Random.Range(0, _otherEnemies.Length)];
        Vector3 position = GetRandomSpawnPosition();
        if (_spawners.TryGetValue(type, out var spawner) && spawner.TrySpawn(position))
        {
            _mageAndKnightAndDragon--;
            return true;
        }
        return false;
    }

    private bool TrySpawnElite()
    {
        if (_elite <= 0)
            return false;

        Vector3 position = GetRandomSpawnPosition();
        if (_spawners[EnemyType.DemonKnight].TrySpawn(position))
        {
            _elite--;
            return true;
        }
        return false;
    }

    private Vector3 GetRandomSpawnPosition()
    {
        int rand = UnityEngine.Random.Range(0, _positions.Count);
        return _positions[rand].position;
    }
}