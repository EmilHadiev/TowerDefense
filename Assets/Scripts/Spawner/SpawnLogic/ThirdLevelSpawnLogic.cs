using System.Collections.Generic;

public class ThirdLevelSpawnLogic : SpawnLogic
{
    private const int SpawnMagesWave = 2;

    private int _skeletons;
    private int _mages;

    public ThirdLevelSpawnLogic(WaveData waveData, IEnumerable<EnemySpawner> spawners) : base(waveData, spawners)
    {
    }

    public override void CalculateNextWave()
    {
        int maxEnemies = WaveData.MaxEnemies;
        int total = maxEnemies;
        int quarter = 4;

        if (WaveData.CurrentWave >= SpawnMagesWave)
        {
            _mages = maxEnemies / quarter;
            _skeletons = maxEnemies - _mages;
        }
        else
        {
            // На первой волне только скелеты
            _skeletons = maxEnemies;
            _mages = 0;
        }

        total -= (_skeletons + _mages);
        _skeletons += total;
    }

    public override bool TrySpawn()
    {
        return TrySpawn(_skeletons, EnemyType.Skeleton)
            || TrySpawn(_mages, EnemyType.Mage);
    }
}