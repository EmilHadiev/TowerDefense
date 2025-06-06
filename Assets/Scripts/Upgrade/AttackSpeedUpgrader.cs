﻿using System;
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

        _data.Cost = GetRaisePrice(_data.Cost);
    }

    public override string GetUpgradeDescription()
    {
        float attackSpeed = (float)Math.Round(Stat.BonusAttackSpeed, 4);

        return $"{attackSpeed} > {attackSpeed - GetTotalValue(attackSpeed)}";
    }

    private void CalculateAttackSpeedValue()
    {
        GetUpgradeDescription();

        Stat.BonusAttackSpeed -= GetTotalValue(Stat.BonusAttackSpeed);
        Debug.Log(Stat.BonusAttackSpeed);
    }

    private float GetTotalValue(float value)
    {
        float total = value / 100 * _data.Value;
        total = (float)Math.Round(total, 3);
        return total;
    }
}