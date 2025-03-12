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
        if (Stat.AttackSpeed == Constants.MaxAttackSpeed)
        {
            return;
        }
        else if (Stat.AttackSpeed <= Constants.MaxAttackSpeed)
        {
            Stat.AttackSpeed = Constants.MaxAttackSpeed;
            return;
        }

        float total = Stat.AttackSpeed / 100 * Data.Value;
        total = (float)Math.Round(total, 3);
        Stat.AttackSpeed -= total;

        Data.Cost = GetRaisePrice(Data.Cost);
    }
}