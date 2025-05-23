public class UpgradePurchaseHandler : IUpgradePurchaseHandler
{
    private readonly ICoinStorage _coinStorage;
    private readonly ISoundContainer _soundContainer;

    public UpgradePurchaseHandler(ICoinStorage coinStorage, ISoundContainer soundContainer)
    {
        _coinStorage = coinStorage;
        _soundContainer = soundContainer;
    }

    public bool TryUpgrade(UpgradeData data)
    {
        if (_coinStorage.TrySpend(data.Cost))
        {
            _soundContainer.Play(SoundName.SpendCoin);
            return true;
        }

        return false;
    }
}