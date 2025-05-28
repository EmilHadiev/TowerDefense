using System.Collections.Generic;
using UnityEngine;
using YG;
using Zenject;

public class UpgradeAdvContainer : AdvertisingContainer, IRewardUpdateCommand
{
    [SerializeField] private RewardedAdvLockTimer _lockTimer;
    [SerializeField] private GameObject _advCooldownContainer;

    private IPlayerSoundContainer _soundContainer;
    private IUpgradePriceCalculator _priceCalculator;
    private ICoinStorage _coinStorage;

    [Inject]
    private void Constructor(IPlayerSoundContainer soundContainer, IEnumerable<UpgradeData> data, ICoinStorage coinStorage)
    {
        _soundContainer = soundContainer;
        _priceCalculator = new UpgradePriceCalculator(data);
        _coinStorage = coinStorage;
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
        SetRewardValueText(GetPrice().ToString());
    }

    protected override void OnClick() => ShowAdv();

    private void ShowAdv()
    {        
        if (_lockTimer.timerComplete)
            Advertising.ShowRewardAdv(GiveCoinsToPlayer);
    }

    private void PlaySpendCoin() => _soundContainer.Play(SoundName.SpendCoin);

    private void OnTimerActivated(bool isOn) => _advCooldownContainer.SetActive(isOn);

    private int GetPrice() => _priceCalculator.CalculatePrice();

    private void GiveCoinsToPlayer()
    {
        _coinStorage.Add(GetPrice());
        PlaySpendCoin();
    }
}