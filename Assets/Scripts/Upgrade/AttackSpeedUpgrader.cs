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
        CalculateAttackSpeedValue();

        Data.Cost = GetRaisePrice(Data.Cost);
    }

    public override string GetUpgradeDescription()
    {
        float attackSpeed = (float)Math.Round(Stat.AttackSpeed, 4);

        return $"{attackSpeed} > {attackSpeed - GetTotalValue(attackSpeed)}";
    }

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