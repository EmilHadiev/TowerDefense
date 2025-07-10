using System;
using System.Collections.Generic;
using UnityEngine;

public class DefaultSpawnLogic : SpawnLogic
{
    private int _skeletons;
    private int _mageAndKnightAndDragon;
    private int _elite;

    public DefaultSpawnLogic(WaveData waveData, IEnumerable<EnemySpawner> spawners) : base(waveData, spawners)
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

    public override bool TrySpawn()
    { 
        return TrySpawn(_skeletons, EnemyType.Skeleton) 
            || TrySpawnRandom(_mageAndKnightAndDragon, EnemyType.Dragon, EnemyType.BlackKnight, EnemyType.Mage, EnemyType.ArmorKnight) 
            || TrySpawn(_elite, EnemyType.DemonKnight);
    }
}