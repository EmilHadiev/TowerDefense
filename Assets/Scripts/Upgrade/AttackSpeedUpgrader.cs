using System;
using UnityEngine;

public class AttackSpeedUpgrader : Upgrader
{
    private const int UpgradePercent = 1;

    protected override UpgradeType UpgradeType { get; }

    public AttackSpeedUpgrader(PlayerStat stat, UpgradeData data) : base(stat, data)
    {
        UpgradeType = UpgradeType.AttackSpeed;
        Debug.Log("ДОДЕЛАТЬ АПГРЕЙД СКОРОСТИ АТАКИ!");
    }

    public override void Upgrade()
    {
        CalculateAttackSpeedValue();
        _data.Cost = GetRaisePrice(_data.Cost);
    }

    public override string GetUpgradeDescription()
    {
        //float attackSpeed = Stat.BonusAttackSpeed;
        //return $"{attackSpeed} > {attackSpeed + UpgradePercent}%";
        return $"НАДО ДОДЕЛАТЬ!";
    }

    private void CalculateAttackSpeedValue()
    {
        GetUpgradeDescription();
        //Stat.BonusAttackSpeed += UpgradePercent;
        //Debug.Log(Stat.BonusAttackSpeed);
    }
}