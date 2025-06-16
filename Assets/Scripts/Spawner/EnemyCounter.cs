using System;

public class EnemyCounter
{
    private readonly WaveData _waveData;

    private int _currentAlive;
    private int _totalSpawned;

    public event Action CapacityReached;
    public event Action AllEnemiesDead;

    public EnemyCounter(WaveData waveData)
    {
        Reset();
        _waveData = waveData;
    }

    public void Add()
    {
        _currentAlive++;
        _totalSpawned++;

        if (_totalSpawned >= _waveData.MaxEnemies)
            CapacityReached?.Invoke();
    }

    public void Remove()
    {
        _currentAlive--;

        if (_currentAlive <= 0 && _totalSpawned >= _waveData.MaxEnemies)
            AllEnemiesDead?.Invoke();
    }

    public void Reset()
    {
        _currentAlive = 0;
        _totalSpawned = 0;
    }
}
