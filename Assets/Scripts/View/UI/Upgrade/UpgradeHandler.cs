using System;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeHandler : MonoBehaviour
{
    [SerializeField] private Button _upgradeDamageButton;
    [SerializeField] private Button _upgradeAttackSpeedButton;

    private ICoinStorage _coinStorage;
    private IPlayerSoundContainer _soundContainer;
    private GunData _gunData;

    public event Action Upgraded;

    private void OnDestroy()
    {
        _upgradeDamageButton?.onClick.RemoveListener(UpgradeDamage);
        _upgradeAttackSpeedButton?.onClick.RemoveListener(UpgradeAttackSpeed);
    }

    public void Initialize(GunData gunData, ICoinStorage coinStorage, IPlayerSoundContainer playerSoundContainer)
    {
        _gunData = gunData;
        _coinStorage = coinStorage;
        _soundContainer = playerSoundContainer;

        _upgradeDamageButton.onClick.AddListener(UpgradeDamage);
        _upgradeAttackSpeedButton.onClick.AddListener(UpgradeAttackSpeed);
    }

    private void UpgradeAttackSpeed()
    {
        if (_coinStorage.TrySpend(Constants.UpgradePrice))
        {
            _gunData.AttackSpeedPercent += _gunData.AttackSpeedPercentageUpgradeValue;
            _soundContainer.Play(SoundName.SpendCoin);
            Upgraded?.Invoke();
        }
    }

    private void UpgradeDamage()
    {
        if (_coinStorage.TrySpend(Constants.UpgradePrice))
        {
            _gunData.BaseDamage += _gunData.DamageUpgradeValue;
            _soundContainer.Play(SoundName.SpendCoin);
            Upgraded?.Invoke();
        }
    }
}
