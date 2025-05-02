public class RewardSystem
{
    private readonly ICoinStorage _coinStorage;
    private readonly EnemyCounter _enemyCounter;

    public RewardSystem(ICoinStorage coinStorage, EnemyCounter enemyCounter)
    {
        _coinStorage = coinStorage;
        _enemyCounter = enemyCounter;
    }

    public void HandleEnemyDeath(int enemyPoint)
    {
        _coinStorage.Add(enemyPoint);
        _enemyCounter.Remove();
    }
}
