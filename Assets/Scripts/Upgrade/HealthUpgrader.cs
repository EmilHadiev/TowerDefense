public class HealthUpgrader : Upgrader
{
    protected override UpgradeType UpgradeType { get; }

    public HealthUpgrader(PlayerStat stat, UpgradeData data) : base(stat, data)
    {
        UpgradeType = UpgradeType.Health;
    }

    public override void Upgrade()
    {
        Stat.HealthProperty.Value += Data.Value;
        Stat.Health = Stat.HealthProperty.Value;
        Stat.MaxHealth = Stat.HealthProperty.Value;

        Data.Cost = GetRaisePrice(Data.Cost);
    }
}