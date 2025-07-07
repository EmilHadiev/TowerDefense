using System;
using UnityEngine;

public abstract class Upgrader
{
    protected readonly PlayerStat Stat;

    private int _countOfUpgraders = 0;

    public Upgrader(PlayerStat stat)
    {
        Stat = stat;
        Debug.Log("Надо доделать и удалить" + nameof(Upgrader));
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