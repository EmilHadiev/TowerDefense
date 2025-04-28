using System;

public interface IBulletPurchaseHandler
{
    event Action<IBulletDescription> Purchased;
    bool TryPurchase(IBulletDescription data);
}