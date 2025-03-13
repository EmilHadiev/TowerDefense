using System;
using UnityEngine;

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

        CalculateAttackSpeedValue();

        Data.Cost = GetRaisePrice(Data.Cost);
    }

    public override string GetUpgradeDescription() => $"{Stat.AttackSpeed} > {Stat.AttackSpeed - GetTotalValue(Stat.AttackSpeed)}";

    private void CalculateAttackSpeedValue()
    {
        GetUpgradeDescription();

        Stat.AttackSpeed -= GetTotalValue(Stat.AttackSpeed);
        Debug.Log(Stat.AttackSpeed);
    }

    private float GetTotalValue(float value)
    {
        float total = value / 100 * Data.Value;
        total = (float)Math.Round(total, 3);
        return total;
    }
}