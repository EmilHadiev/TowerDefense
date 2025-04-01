using System;

class EnemyCounter
{
    private int _currentAlive;
    private int _totalSpawned;

    public event Action CapacityReached;
    public event Action AllEnemiesDead;

    public EnemyCounter()
    {
        Reset();
    }

    public void Add()
    {
        _currentAlive++;
        _totalSpawned++;

        if (_totalSpawned >= Constants.MaxEnemies)
            CapacityReached?.Invoke();
    }

    public void Remove()
    {
        _currentAlive--;

        if (_currentAlive <= 0 && _totalSpawned >= Constants.MaxEnemies)
            AllEnemiesDead?.Invoke();
    }

    public void Reset()
    {
        _currentAlive = 0;
        _totalSpawned = 0;
    }
}
