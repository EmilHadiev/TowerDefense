using System;
using System.Collections.Generic;

public class BossLevelSpawnLogic : SpawnLogic
{
    private int _skeletons;
    private int _others;
    private int _elite;

    public BossLevelSpawnLogic(WaveData waveData, IEnumerable<EnemySpawner> spawners) : base(waveData, spawners)
    {
    }

    public override void CalculateNextWave()
    {
        int maxEnemies = WaveData.MaxEnemies;
        int total = maxEnemies;

        _skeletons = Convert.ToInt32(maxEnemies / 2);
        int others = maxEnemies - _skeletons;

        if (WaveData.IsFinalWave)
        {
            _elite = 1;
            _others = others - _elite;
        }
        else
        {
            _elite = 0;
            _others = others;
        }

        total -= (_skeletons + _others + _elite);
        _others += total;
    }

    public override bool TrySpawn()
    { 
        return TrySpawn(_skeletons, EnemyType.Skeleton) 
            || TrySpawnRandom(_others, EnemyType.Dragon, EnemyType.BlackKnight, EnemyType.Mage, EnemyType.ArmorKnight) 
            || TrySpawn(_elite, EnemyType.DemonKnight);
    }
}