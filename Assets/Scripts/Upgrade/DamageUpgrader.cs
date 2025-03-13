using System;

public class DamageUpgrader : Upgrader
{
    protected override UpgradeType UpgradeType { get; }

    public DamageUpgrader(PlayerStat stat, UpgradeData data) : base(stat, data)
    {
        UpgradeType = UpgradeType.Damage;
    }

    public override string GetUpgradeDescription() => $"{Stat.Damage} > {Stat.Damage + Data.Value}";

    public override void Upgrade()
    {
        GetUpgradeDescription();

        Stat.DamageProperty.Value += Data.Value;
        Stat.Damage += Data.Value;

        Data.Cost = GetRaisePrice(Data.Cost);
    }
}
