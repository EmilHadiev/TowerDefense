using System;
using System.Collections.Generic;

public class UpgradePriceCalculator : IUpgradePriceCalculator
{
    private readonly IEnumerable<UpgradeData> _data;

    public UpgradePriceCalculator(IEnumerable<UpgradeData> data)
    {
        _data = data;
    }

    public int CalculatePrice()
    {
        float totalPrice = 0;

        foreach (var data in _data)
            totalPrice += data.Cost;

        totalPrice = totalPrice * Constants.AdvUpgradeCoefficient + Constants.UpgradeStartPrice;
        return Convert.ToInt32(totalPrice);
    }
}
