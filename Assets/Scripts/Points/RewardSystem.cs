public class RewardSystem
{
    private readonly ICoinStorage _coinStorage;
    private readonly EnemyCounter _enemyCounter;
    private readonly IProfitContainer _profitContainer;

    public RewardSystem(ICoinStorage coinStorage, EnemyCounter enemyCounter, IProfitContainer profitContainer)
    {
        _coinStorage = coinStorage;
        _enemyCounter = enemyCounter;
        _profitContainer = profitContainer;
    }

    public void HandleEnemyDeath(int enemyPoint)
    {
        _coinStorage.Add(enemyPoint);
        _profitContainer.IncreaseProfits(enemyPoint);
        _enemyCounter.Remove();
    }
}
