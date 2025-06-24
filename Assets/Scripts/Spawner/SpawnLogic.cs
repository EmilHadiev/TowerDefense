using System.Collections.Generic;

public class SpawnLogic
{
    private readonly WaveData _waveData;
    private readonly IEnumerable<EnemySpawner> _spawners;

    public SpawnLogic(WaveData waveData, IEnumerable<EnemySpawner> spawners)
    {
        _waveData = waveData;
        _spawners = spawners;
    }

    public void SpawnEnemy()
    {

    }
}