class EnemyUpgradeState : ILevelState
{
    private readonly EnemyUpgrader _upgrader;
    private readonly ILevelSwitcher _switcher;

    public EnemyUpgradeState(EnemyUpgrader enemyUpgrader, ILevelSwitcher levelSwitcher)
    {
        _upgrader = enemyUpgrader;
        _switcher = levelSwitcher;
    }

    public void Enter()
    {        
        _upgrader.TryUpgrade();
        _switcher.SwitchTo<EnemySpawnerContainer>();
    }

    public void Exit() => _upgrader.LevelUp();
}