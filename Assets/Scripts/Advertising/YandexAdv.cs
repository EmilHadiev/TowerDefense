using System;
using UnityEngine;
using YG;

public class YandexAdv : IAdvertising
{
    private const int Coins = 100;

    private readonly ICoinStorage _coinStorage;

    public string RewardValue => Coins.ToString();

    public YandexAdv(ICoinStorage coinStorage)
    {
        _coinStorage = coinStorage;
    }

    public void StickyBannerToggle(bool isOn) => YG2.StickyAdActivity(isOn);

    public bool TryShowInterstitialAdv()
    {
        throw new System.NotImplementedException();
    }

    public void ShowRewardAdv(AdvType advType, string rewardValue)
    {
        string id = "";
        YG2.RewardedAdvShow(id, () =>
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
        });
    }

    private int TryConvertToInt(string rewardValue)
    {
        if (int.TryParse(rewardValue, out int result))
            return result;

        throw new ArgumentException(nameof(rewardValue));
    }
}