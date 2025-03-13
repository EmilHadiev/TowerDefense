using System;

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
        Stat.MaxHealth += Data.Value;

        Data.Cost = GetRaisePrice(Data.Cost);
    }

    public override string GetUpgradeDescription() => $"{Stat.MaxHealth} > {Stat.MaxHealth + Data.Value}";
}