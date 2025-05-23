using System.Collections.Generic;
using UnityEngine;
using YG;
using Zenject;

public class UpgradeAdvContainer : AdvertisingContainer, IRewardUpdateCommand
{
    [SerializeField] private RewardedAdvLockTimer _lockTimer;
    [SerializeField] private GameObject _advCooldownContainer;

    private const AdvType Type = AdvType.Coin;
    private ISoundContainer _soundContainer;
    private IUpgradePriceCalculator _priceCalculator;

    [Inject]
    private void Constructor(ISoundContainer soundContainer, IEnumerable<UpgradeData> data)
    {
        _soundContainer = soundContainer;
        _priceCalculator = new UpgradePriceCalculator(data);
    }

    private void Start()
    {
        UpdateReward();
        _lockTimer.Activated += OnTimerActivated;
    }

    private void OnDestroy()
    {
        _lockTimer.Activated -= OnTimerActivated;
    }

    public void UpdateReward()
    {
        SetRewardValueText(GetPrice());
    }

    protected override void OnClick() => ShowAdv();

    private void ShowAdv()
    {        
        if (_lockTimer.timerComplete)
            Advertising.ShowRewardAdv(Type, GetPrice(), PlaySpendCoin);
    }

    private void PlaySpendCoin() => _soundContainer.Play(SoundName.SpendCoin);

    private void OnTimerActivated(bool isOn) => _advCooldownContainer.SetActive(isOn);

    private string GetPrice() => _priceCalculator.CalculatePrice().ToString();
}