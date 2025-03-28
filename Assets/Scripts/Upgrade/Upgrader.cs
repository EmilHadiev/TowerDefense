using System;
using UnityEngine;

public abstract class Upgrader
{
    protected readonly PlayerStat Stat;
    private int _countOfUpgraders = 0;

    protected abstract UpgradeType UpgradeType { get; }

    public readonly UpgradeData Data;

    public Upgrader(PlayerStat stat, UpgradeData data)
    {
        Stat = stat;
        Data = data;
    }

    public abstract void Upgrade();

    public abstract string GetUpgradeDescription();

    protected int GetRaisePrice(int currentPrice)
    {
        int additionalPercent = Convert.ToInt32((float)currentPrice / 100 * Constants.AdditionalUpgradePercentage);
        _countOfUpgraders++;
        Debug.Log($"Количество улучшений: {_countOfUpgraders}");
        return currentPrice + additionalPercent + Constants.AdditionalUpgradePrice;
    }
}