public class HealthUpgrader : Upgrader
{
    protected override UpgradeType UpgradeType { get; }

    public HealthUpgrader(PlayerStat stat, UpgradeData data) : base(stat, data)
    {
        UpgradeType = UpgradeType.Health;
    }

    public override void Upgrade() => Stat.Health.Value += Data.Value;
}