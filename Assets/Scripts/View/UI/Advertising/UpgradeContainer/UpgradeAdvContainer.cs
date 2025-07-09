using System.Collections.Generic;
using UnityEngine;
using YG;
using Zenject;

public class UpgradeAdvContainer : AdvertisingContainer
{
    [SerializeField] private RewardedAdvLockTimer _lockTimer;
    [SerializeField] private GameObject _advCooldownContainer;

    private IPlayerSoundContainer _soundContainer;
    private ICoinStorage _coinStorage;

    [Inject]
    private void Constructor(IPlayerSoundContainer soundContainer, ICoinStorage coinStorage)
    {
        _soundContainer = soundContainer;
        _coinStorage = coinStorage;
    }

    private void Start()
    {
        SetRewardValueText(Constants.UpgradePrice.ToString());
        _lockTimer.Activated += OnTimerActivated;
    }

    private void OnDestroy()
    {
        _lockTimer.Activated -= OnTimerActivated;
    }

    protected override void OnClick() => ShowAdv();

    private void ShowAdv()
    {        
        if (_lockTimer.timerComplete)
            Advertising.ShowRewardAdv(GiveCoinsToPlayer);
    }

    private void PlaySpendCoin() => _soundContainer.Play(SoundName.SpendCoin);

    private void OnTimerActivated(bool isOn) => _advCooldownContainer.SetActive(isOn);

    private void GiveCoinsToPlayer()
    {
        _coinStorage.Add(Constants.UpgradePrice);
        PlaySpendCoin();
    }
}