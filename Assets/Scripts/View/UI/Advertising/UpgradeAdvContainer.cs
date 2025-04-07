using System;
using System.Collections.Generic;
using Zenject;
using UnityEngine;
using YG;

public class UpgradeAdvContainer : AdvertisingContainer
{
    [SerializeField] private RewardedAdvLockTimer _lockTimer;
    [SerializeField] private GameObject _advCooldownContainer;

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
        CalculatePrice();
        SetText(_rewardValue);
    }

    protected override void OnClick() => ShowAdv();

    private void PlaySpendCoin() => _soundContainer.Play(SoundType.SpendCoin);

    private void CalculatePrice()
    {
        float totalPrice = 0;

        foreach (var data in _data)
            totalPrice += data.Cost;

        totalPrice = totalPrice * Constants.AdvUpgradeCoefficient + Constants.UpgradeStartPrice;
        _rewardValue = Convert.ToInt32(totalPrice).ToString();
    }

    private void ShowAdv()
    {
        
        if (_lockTimer.timerComplete)
            Advertising.ShowRewardAdv(Type, _rewardValue, PlaySpendCoin);
    }

    private void OnTimerActivated(bool isOn) => _advCooldownContainer.SetActive(isOn);
}