using System;

public class AttackSpeedUpgrader : Upgrader
{
    protected override UpgradeType UpgradeType { get; }

    public AttackSpeedUpgrader(PlayerStat stat, UpgradeData data) : base(stat, data)
    {
        UpgradeType = UpgradeType.AttackSpeed;
    }

    public override void Upgrade()
    {
        float total = Stat.AttackSpeed.Value / 100 * Data.Value;
        total = (float)Math.Round(total, 3);
        Stat.AttackSpeed.Value -= total;
    }
}