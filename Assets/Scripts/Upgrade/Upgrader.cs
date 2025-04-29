using System;
using UnityEngine;

public abstract class Upgrader : IUpgrader
{
    protected readonly PlayerStat Stat;
    protected readonly UpgradeData _data;

    private int _countOfUpgraders = 0;

    protected abstract UpgradeType UpgradeType { get; }

    public UpgradeData Data => _data;

    public Upgrader(PlayerStat stat, UpgradeData data)
    {
        Stat = stat;
        _data = data;
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