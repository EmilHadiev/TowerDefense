using System;

public class Purchaser : IPurchaser
{
    private readonly ICoinStorage _coinStorage;

    public Purchaser(ICoinStorage coinStorage)
    {
        _coinStorage = coinStorage;
    }

    public bool TryPurchase(IPurchasable purchasable, Action action = null)
    {
        if (purchasable.IsPurchased)
            return false;

        if (_coinStorage.TrySpend(purchasable.Price))
        {
            purchasable.IsPurchased = true;

            action?.Invoke();
            return true;
        }

        return false;
    }
}