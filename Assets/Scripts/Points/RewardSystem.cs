public class RewardSystem
{
    private readonly ICoinStorage _coinStorage;
    private readonly EnemyCounter _enemyCounter;
    private readonly IProfitContainer _profitContainer;
    private readonly LevelTracker _levelTracker;
    private readonly IComboSystem _comboSystem;

    public RewardSystem(ICoinStorage coinStorage, EnemyCounter enemyCounter, IProfitContainer profitContainer, LevelTracker tracker, IComboSystem comboSystem)
    {
        _coinStorage = coinStorage;
        _enemyCounter = enemyCounter;
        _profitContainer = profitContainer;
        _levelTracker = tracker;
        _comboSystem = comboSystem;
    }

    public void HandleEnemyDeath(int enemyPoint)
    {
        if (_levelTracker.IsNotCompletedLevel)
        {
            _coinStorage.Add(enemyPoint);
            _profitContainer.IncreaseProfits(enemyPoint);
            _comboSystem.Add();
        }

        _enemyCounter.Remove();
    }
}
