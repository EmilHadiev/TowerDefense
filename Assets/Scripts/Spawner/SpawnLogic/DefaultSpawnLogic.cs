using System;
using System.Collections.Generic;

public class DefaultSpawnLogic : SpawnLogic
{
    private int _skeletons;
    private int _others;

    public DefaultSpawnLogic(WaveData waveData, IEnumerable<EnemySpawner> spawners) : base(waveData, spawners)
    {
    }

    public override void CalculateNextWave()
    {
        int maxEnemies = WaveData.MaxEnemies;
        int total = maxEnemies;

        _skeletons = Convert.ToInt32(maxEnemies / 2);
        int others = maxEnemies - _skeletons;

        _others = others;

        total -= (_skeletons + _others);
        _others += total;
    }

    public override bool TrySpawn()
    {
        return TrySpawn(_skeletons, EnemyType.Skeleton)
            || TrySpawnRandom(_others, EnemyType.Dragon, EnemyType.BlackKnight, EnemyType.Mage, EnemyType.ArmorKnight);
    }
}