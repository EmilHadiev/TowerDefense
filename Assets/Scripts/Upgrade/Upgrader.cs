using System;
using UnityEngine;

public abstract class Upgrader
{
    protected readonly PlayerStat Stat;

    protected abstract UpgradeType UpgradeType { get; }

    public readonly UpgradeData Data;

    public Upgrader(PlayerStat stat, UpgradeData data)
    {
        Stat = stat;
        Data = data;
    }

    public abstract void Upgrade();

    protected int GetRaisePrice(int currentPrice)
    {
        int additionalPercent = Convert.ToInt32((float)currentPrice / 100 * Constants.AdditionalUpgradePercentage);
        Debug.Log(additionalPercent);
        return currentPrice + additionalPercent + Constants.AdditionalUpgradePrice;
    }
}