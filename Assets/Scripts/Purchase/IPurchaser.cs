using System;

public interface IPurchaser
{
    public bool TryPurchase(IPurchasable purchasable, Action action = null);
}
