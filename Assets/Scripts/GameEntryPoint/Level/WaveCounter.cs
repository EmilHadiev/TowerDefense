using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;

public class WaveCounter : IInitializable, IDisposable
{
    private readonly WaveData _waveData;
    private readonly EnemyCounter _enemyCounter;
    private readonly IGameOver _gameOver;

    public WaveCounter(WaveData waveData, EnemyCounter enemyCounter, IGameOver gameOver)
    {
        _waveData = waveData;
        _enemyCounter = enemyCounter;
        _gameOver = gameOver;
    }

    public void Initialize()
    {
        _enemyCounter.AllEnemiesDead += WaveEnded;
    }

    public void Dispose()
    {
        _enemyCounter.AllEnemiesDead += WaveEnded;
    }

    private void WaveEnded()
    {
        _waveData.PrepareNextWave();

        if (_waveData.IsWaveEnded)
            _gameOver.GameCompleted();
    }
}