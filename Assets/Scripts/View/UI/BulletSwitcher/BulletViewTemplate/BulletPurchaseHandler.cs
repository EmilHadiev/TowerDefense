using System;

public class BulletPurchaseHandler : IBulletPurchaseHandler
{
    private readonly ICoinStorage _coinStorage;
    private readonly ISoundContainer _soundContainer;

    public event Action<IBulletDescription> Purchased;

    public BulletPurchaseHandler(ICoinStorage coinStorage, ISoundContainer soundContainer)
    {
        _coinStorage = coinStorage;
        _soundContainer = soundContainer;
    }

    public bool TryPurchase(IBulletDescription data)
    {
        if (data.IsPurchased)
            return false;

        if (_coinStorage.TrySpend(data.Price))
        {
            _soundContainer.Play(SoundType.SpendCoin);
            data.IsPurchased = true;

            Purchased?.Invoke(data);
            return true;
        }

        return false;
    }
}