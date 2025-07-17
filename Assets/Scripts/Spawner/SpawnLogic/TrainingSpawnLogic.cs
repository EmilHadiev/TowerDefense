using System.Collections.Generic;

public class TrainingSpawnLogic : SpawnLogic
{
    private const int ArmorKnightsCount = 2;

    private int _skeletons;
    private int _armorKnights;

    public TrainingSpawnLogic(WaveData waveData, IEnumerable<EnemySpawner> spawners) : base(waveData, spawners)
    {
        
    }

    public override void CalculateNextWave()
    {
        _skeletons = WaveData.MaxEnemies;
        _armorKnights = 0;

        if (WaveData.IsFinalWave)
        {
            _armorKnights = ArmorKnightsCount;
            _skeletons -= ArmorKnightsCount;
        }

    }

    public override bool TrySpawn()
    {
        return TrySpawn(_skeletons, EnemyType.Skeleton) 
            || TrySpawn(_armorKnights, EnemyType.ArmorKnight);
    }
}