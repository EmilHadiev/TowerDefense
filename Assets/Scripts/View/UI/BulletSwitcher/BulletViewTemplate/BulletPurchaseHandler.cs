using System;

public class BulletPurchaseHandler : IBulletPurchaseHandler
{
    private readonly ICoinStorage _coinStorage;
    private readonly IPlayerSoundContainer _soundContainer;

    public event Action<IBulletDescription> Purchased;

    public BulletPurchaseHandler(ICoinStorage coinStorage, IPlayerSoundContainer soundContainer)
    {
        _coinStorage = coinStorage;
        _soundContainer = soundContainer;
    }

    public bool TryPurchase(IBulletDescription data)
    {
        return false;
    }
}