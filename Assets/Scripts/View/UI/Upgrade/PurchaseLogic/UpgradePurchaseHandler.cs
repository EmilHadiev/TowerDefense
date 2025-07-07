using UnityEngine;

public class UpgradePurchaseHandler : IUpgradePurchaseHandler
{
    private readonly ICoinStorage _coinStorage;
    private readonly IPlayerSoundContainer _soundContainer;

    public UpgradePurchaseHandler(ICoinStorage coinStorage, IPlayerSoundContainer soundContainer)
    {
        _coinStorage = coinStorage;
        _soundContainer = soundContainer;
        Debug.Log("Надо доделать " + nameof(UpgradePurchaseHandler));
    }

    public bool TryUpgrade(/*UpgradeData data*/)
    {
        /*/if (_coinStorage.TrySpend(data.Cost))
        {
            _soundContainer.Play(SoundName.SpendCoin);
            return true;
        }*/

        return false;
    }
}