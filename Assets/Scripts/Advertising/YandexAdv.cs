using System;
using YG;
using Zenject;

public class YandexAdv : IAdvertising, IInitializable, IDisposable
{
    private readonly ICoinStorage _coinStorage;
    private readonly GameplayMarkup _markup;

    public YandexAdv(ICoinStorage coinStorage, GameplayMarkup markup)
    {
        _coinStorage = coinStorage;
        _markup = markup;
    }

    public void Initialize() => YG2.onCloseAnyAdv += OnCloseAdv;

    public void Dispose() => YG2.onCloseAnyAdv -= OnCloseAdv;

    public void StickyBannerToggle(bool isOn) => YG2.StickyAdActivity(isOn);

    public void ShowInterstitialAdv() => YG2.InterstitialAdvShow();


    public void ShowRewardAdv(AdvType advType, string rewardValue = "", Action callBack = null)
    {
        YG2.RewardedAdvShow(Constants.RewardID, () =>
        {
            switch (advType)
            {
                case AdvType.Coin:
                    _coinStorage.Add(TryConvertToInt(rewardValue));
                    break;
                case AdvType.Resurrect:
                    callBack?.Invoke();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(advType));
            }

            callBack?.Invoke();
        });
    }

    private int TryConvertToInt(string rewardValue)
    {
        if (int.TryParse(rewardValue, out int result))
            return result;

        throw new ArgumentException(nameof(rewardValue));
    }

    private void OnCloseAdv() => _markup.Stop();
}