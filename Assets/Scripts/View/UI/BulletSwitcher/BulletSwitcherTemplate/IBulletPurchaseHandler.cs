using System;

public interface IBulletPurchaseHandler
{
    bool TryPurchase(IBulletDescription data);
}