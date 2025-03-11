using System;
using UnityEngine;
using YG;

public class YandexAdv : IAdvertising
{
    private const int Coins = 100;

    private readonly ICoinStorage _coinStorage;
    private readonly ISoundContainer _soundContainer;

    public string RewardValue => Coins.ToString();

    public YandexAdv(ICoinStorage coinStorage, ISoundContainer soundContainer)
    {
        _coinStorage = coinStorage;
        _soundContainer = soundContainer;
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
                    _soundContainer.Play(SoundType.SpendCoin);
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