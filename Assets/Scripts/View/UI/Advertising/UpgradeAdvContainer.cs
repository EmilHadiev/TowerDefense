using System;
using System.Collections.Generic;
using Zenject;

public class UpgradeAdvContainer : AdvertisingContainer
{
    private const AdvType Type = AdvType.Coin;
    private ISoundContainer _soundContainer;
    private IEnumerable<UpgradeData> _data;

    private string _rewardValue;

    [Inject]
    private void Constructor(ISoundContainer soundContainer, IEnumerable<UpgradeData> data)
    {
        _soundContainer = soundContainer;
        _data = data;
    }

    private void Start() => UpdateReward();

    public void UpdateReward()
    {
        CalculatePrice();
        SetText(_rewardValue);
    }

    protected override void OnClick() => Advertising.ShowRewardAdv(Type, _rewardValue, PlaySpendCoin);

    private void PlaySpendCoin() => _soundContainer.Play(SoundType.SpendCoin);

    private void CalculatePrice()
    {
        float totalPrice = 0;

        foreach (var data in _data)
            totalPrice += data.Cost;

        totalPrice = totalPrice * Constants.AdvUpgradeCoefficient + Constants.UpgradeStartPrice;
        _rewardValue = Convert.ToInt32(totalPrice).ToString();
    }
}