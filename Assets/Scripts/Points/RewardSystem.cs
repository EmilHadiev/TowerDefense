public class RewardSystem
{
    private readonly ICoinStorage _coinStorage;
    private readonly EnemyCounter _enemyCounter;
    private readonly IProfitContainer _profitContainer;
    private readonly LevelTracker _levelTracker;

    public RewardSystem(ICoinStorage coinStorage, EnemyCounter enemyCounter, IProfitContainer profitContainer, LevelTracker tracker)
    {
        _coinStorage = coinStorage;
        _enemyCounter = enemyCounter;
        _profitContainer = profitContainer;
        _levelTracker = tracker;
    }

    public void HandleEnemyDeath(int enemyPoint)
    {
        _enemyCounter.Remove();

        if (_levelTracker.IsNotCompletedLevel == false)
            return;

        _coinStorage.Add(enemyPoint);
        _profitContainer.IncreaseProfits(enemyPoint);
    }
}
