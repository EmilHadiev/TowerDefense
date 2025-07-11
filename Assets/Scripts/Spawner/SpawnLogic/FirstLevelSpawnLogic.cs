using System.Collections.Generic;

public class FirstLevelSpawnLogic : SpawnLogic
{
    private const int SpawnArmorKnightsWave = 2;

    private int _skeletons;
    private int _armorKnights;
    private int _dragons;

    public FirstLevelSpawnLogic(WaveData waveData, IEnumerable<EnemySpawner> spawners) : base(waveData, spawners)
    {
    }

    public override void CalculateNextWave()
    {
        int maxEnemies = WaveData.MaxEnemies;
        int total = maxEnemies;
        int quarter = 4;

        if (WaveData.IsFinalWave)
        {
            _armorKnights = maxEnemies / quarter;
            _dragons = maxEnemies / quarter;
            _skeletons = maxEnemies - _armorKnights - _dragons;
        }
        else if (WaveData.CurrentWave >= SpawnArmorKnightsWave)
        {
            _armorKnights = maxEnemies / quarter;
            _skeletons = maxEnemies - _armorKnights;
            _dragons = 0;
        }
        else
        {
            _skeletons = maxEnemies;
            _armorKnights = 0;
            _dragons = 0;
        }

        total -= (_skeletons + _armorKnights + _dragons);
        _skeletons += total;
    }

    public override bool TrySpawn()
    {
        return TrySpawn(_skeletons, EnemyType.Skeleton)
            || TrySpawn(_armorKnights, EnemyType.ArmorKnight)
            || TrySpawn(_dragons, EnemyType.Dragon);
    }
}