using System.Collections.Generic;

public class SecondLevelSpawnLogic : SpawnLogic
{
    private const int SpawnArmorKnightsWave = 2;

    private int _skeletons;
    private int _armorKnights;
    private int _dragons;
    private int _blackKnights;

    public SecondLevelSpawnLogic(WaveData waveData, IEnumerable<EnemySpawner> spawners) : base(waveData, spawners)
    {
    }

    public override void CalculateNextWave()
    {
        int maxEnemies = WaveData.MaxEnemies;
        int total = maxEnemies;

        if (WaveData.IsFinalWave)
        {
            int strongEnemies = maxEnemies / 2;
            _skeletons = maxEnemies - strongEnemies;

            int eachType = strongEnemies / 3;
            _armorKnights = eachType;
            _dragons = eachType;
            _blackKnights = eachType;

            int remainder = strongEnemies - (eachType * 3);
            _blackKnights += remainder;
        }
        else if (WaveData.CurrentWave >= SpawnArmorKnightsWave)
        {
            int strongEnemies = maxEnemies / 2;
            _skeletons = maxEnemies - strongEnemies;

            _armorKnights = strongEnemies / 2;
            _dragons = strongEnemies - _armorKnights;
            _blackKnights = 0;
        }
        else
        {
            _skeletons = maxEnemies;
            _armorKnights = 0;
            _dragons = 0;
            _blackKnights = 0;
        }

        total -= (_skeletons + _armorKnights + _dragons + _blackKnights);
        _skeletons += total;
    }

    public override bool TrySpawn()
    {
        return TrySpawn(_skeletons, EnemyType.Skeleton)
            || TrySpawn(_dragons, EnemyType.Dragon)
            || TrySpawn(_armorKnights, EnemyType.ArmorKnight)
            || TrySpawn(_blackKnights, EnemyType.BlackKnight);
    }
}