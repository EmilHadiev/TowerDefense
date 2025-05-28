using System;
using YG;
using Zenject;

public class YandexAdv : IAdvertising, IInitializable, IDisposable
{
    private readonly GameplayMarkup _markup;

    public YandexAdv(GameplayMarkup markup)
    {
        _markup = markup;
    }

    public void Initialize() => YG2.onCloseAnyAdv += OnCloseAdv;

    public void Dispose() => YG2.onCloseAnyAdv -= OnCloseAdv;

    public void StickyBannerToggle(bool isOn) => YG2.StickyAdActivity(isOn);

    public void ShowInterstitialAdv() => YG2.InterstitialAdvShow();


    public void ShowRewardAdv(Action callBack)
    {
        YG2.RewardedAdvShow(Constants.RewardID, () =>
        {
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