using System;

public class DamageUpgrader : Upgrader
{
    protected override UpgradeType UpgradeType { get; }

    public DamageUpgrader(PlayerStat stat, UpgradeData data) : base(stat, data)
    {
        UpgradeType = UpgradeType.Damage;
    }

    public override string GetUpgradeDescription() => $"{Stat.Damage} > {Stat.Damage + _data.Value}";

    public override void Upgrade()
    {
        GetUpgradeDescription();

        Stat.DamageProperty.Value += _data.Value;
        Stat.Damage += _data.Value;

        _data.Cost = GetRaisePrice(_data.Cost);
    }
}
