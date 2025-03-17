using System;
using UnityEngine;
using YG;
using Zenject;

public class YandexAdv : IAdvertising, IInitializable, IDisposable
{
    private const int Coins = 100;

    private readonly ICoinStorage _coinStorage;
    private readonly GameplayerMarkup _markup;

    public string RewardValue => Coins.ToString();

    public YandexAdv(ICoinStorage coinStorage, GameplayerMarkup markup)
    {
        _coinStorage = coinStorage;
        _markup = markup;
    }

    public void Initialize()
    {
        YG2.onCloseRewardedAdv += OnRewardAdvClosed;
        YG2.onErrorRewardedAdv += OnRewardAdvClosed;
    }

    public void Dispose()
    {
        YG2.onCloseRewardedAdv -= OnRewardAdvClosed;
        YG2.onErrorRewardedAdv -= OnRewardAdvClosed;
    }

    public void StickyBannerToggle(bool isOn) => YG2.StickyAdActivity(isOn);

    public bool TryShowInterstitialAdv()
    {
        throw new System.NotImplementedException();
    }

    public void ShowRewardAdv(AdvType advType, string rewardValue = "", Action callBack = null)
    {
        _markup.Stop();

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

            callBack?.Invoke();
        });
    }

    private int TryConvertToInt(string rewardValue)
    {
        if (int.TryParse(rewardValue, out int result))
            return result;

        throw new ArgumentException(nameof(rewardValue));
    }

    private void OnRewardAdvClosed() => _markup.Start();
}