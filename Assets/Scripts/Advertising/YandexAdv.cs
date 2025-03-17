using System;
using UnityEngine;
using YG;

public class YandexAdv : IAdvertising
{
    private readonly ICoinStorage _coinStorage;

    public YandexAdv(ICoinStorage coinStorage)
    {
        _coinStorage = coinStorage;
    }

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
                    Debug.Log("Доделать восркшение!");
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
}