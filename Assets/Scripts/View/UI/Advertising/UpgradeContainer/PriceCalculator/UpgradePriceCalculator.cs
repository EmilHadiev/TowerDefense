using System;
using UnityEngine;

public class UpgradePriceCalculator : IUpgradePriceCalculator
{
    public UpgradePriceCalculator()
    {
        Debug.Log("����������� ��������� ���� �����? �������� ������� + " + nameof(UpgradePriceCalculator));
    }

    public int CalculatePrice()
    {
        float totalPrice = 0;

        //foreach (var data in _data)
            //totalPrice += data.Cost;

        totalPrice = totalPrice * Constants.AdvUpgradeCoefficient + Constants.UpgradeStartPrice;
        return Convert.ToInt32(totalPrice);
    }
}
