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
        Stat.HealthProperty.Value += _data.Value;
        Stat.MaxHealth += _data.Value;

        _data.Cost = GetRaisePrice(_data.Cost);
    }

    public override string GetUpgradeDescription() => $"{Stat.MaxHealth} > {Stat.MaxHealth + _data.Value}";
}