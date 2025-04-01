class UpgradeLevelState : ILevelState
{
    private readonly EnemyUpgrader _upgrader;
    private readonly ILevelSwitcher _switcher;

    public UpgradeLevelState(EnemyUpgrader enemyUpgrader, ILevelSwitcher levelSwitcher)
    {
        _upgrader = enemyUpgrader;
        _switcher = levelSwitcher;
    }

    public void Enter()
    {
        _upgrader.LevelUp();
        _switcher.SwitchTo<EnemySpawnerContainer>();
    }

    public void Exit() => _upgrader.Upgrade();
}