class EnemyUpgradeState : ILevelState
{
    private readonly EnemyUpgrader _upgrader;
    private readonly ILevelStateSwitcher _switcher;

    public EnemyUpgradeState(EnemyUpgrader enemyUpgrader, ILevelStateSwitcher levelSwitcher)
    {
        _upgrader = enemyUpgrader;
        _switcher = levelSwitcher;
    }

    public void Enter()
    {        
        _upgrader.TryUpgrade();
        _switcher.SwitchState<EnemySpawnerContainer>();
    }

    public void Exit() => _upgrader.LevelUp();
}
