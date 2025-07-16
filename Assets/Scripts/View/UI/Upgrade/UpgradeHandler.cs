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
    private IGunPlace _gunPlace;

    public event Action Upgraded;

    public event Action DamageFilled;
    public event Action AttackSpeedFilled;

    private void OnDestroy()
    {
        _upgradeDamageButton?.onClick.RemoveListener(UpgradeDamage);
        _upgradeAttackSpeedButton?.onClick.RemoveListener(TryUpgradeAttackSpeed);
    }

    public void Initialize(GunData gunData, ICoinStorage coinStorage, IPlayerSoundContainer playerSoundContainer, IGunPlace gunPlace)
    {
        _gunPlace = gunPlace;
        _gunData = gunData;
        _coinStorage = coinStorage;
        _soundContainer = playerSoundContainer;

        _upgradeDamageButton.onClick.AddListener(UpgradeDamage);
        _upgradeAttackSpeedButton.onClick.AddListener(TryUpgradeAttackSpeed);

        CheckAttackSpeedMaxLevel();
        CheckDamageMaxLevel();
    }

    private void TryUpgradeAttackSpeed()
    {
        if (_coinStorage.TrySpend(Constants.StartUpgradePrice))
        {
            _gunData.AttackSpeedPercent += _gunData.AttackSpeedPercentageUpgradeValue;
            _gunData.AttackSpeedLevel += 1;
            _soundContainer.Play(SoundName.SpendCoin);
            _gunPlace.UpdateCurrentGunAttackSpeed();
            Upgraded?.Invoke();
        }

        CheckAttackSpeedMaxLevel();
    }

    private void CheckAttackSpeedMaxLevel()
    {
        if (_gunData.IsAttackSpeedMaxLevel == false)
        {
            AttackSpeedFilled?.Invoke();
            _upgradeAttackSpeedButton.gameObject.SetActive(false);
        }
    }

    private void UpgradeDamage()
    {
        if (_coinStorage.TrySpend(Constants.StartUpgradePrice))
        {
            _gunData.BaseDamage += _gunData.DamageUpgradeValue;
            _gunData.DamageLevel += 1;
            _soundContainer.Play(SoundName.SpendCoin);
            Upgraded?.Invoke();
        }

        CheckDamageMaxLevel();
    }

    private void CheckDamageMaxLevel()
    {
        if (_gunData.IsDamageMaxLevel == false)
        {
            DamageFilled?.Invoke();
            _upgradeDamageButton.gameObject.SetActive(false);
        }
    }
}
