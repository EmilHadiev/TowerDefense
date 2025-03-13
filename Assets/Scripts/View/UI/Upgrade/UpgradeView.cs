using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UpgradeView : MonoBehaviour
{
    [SerializeField] private Image _upgradeImage;
    [SerializeField] private TMP_Text _upgradeNameText;
    [SerializeField] private TMP_Text _upgradeDescriptionText;
    [SerializeField] private TMP_Text _costText;
    [SerializeField] private Button _buyButton;

    private ICoinStorage _coinStorage;
    private ISoundContainer _soundContainer;
    private Upgrader _upgrader;

    private Action _updateReward;

    private void OnEnable()
    {
        _buyButton.onClick.AddListener(OnClicked);
    }

    private void OnDisable()
    {
        _buyButton.onClick.RemoveListener(OnClicked);
    }

    public void Initialize(ICoinStorage coinStorage, ISoundContainer soundContainer, Upgrader upgrader, Action updateReward)
    {
        _coinStorage = coinStorage;
        _soundContainer = soundContainer;
        _upgrader = upgrader;
        _updateReward = updateReward;
        Show(upgrader.Data);
    }

    private void Show(UpgradeData data)
    {
        _upgradeImage.sprite = data.Sprite;
        _upgradeNameText.text = data.Name;
        ShowUpgradeDescription(_upgrader.GetUpgradeDescription());
        _costText.text = data.Cost.ToString();
    }

    private void OnClicked()
    {
        if (_coinStorage.TrySpend(_upgrader.Data.Cost))
        {
            _upgrader.Upgrade();            
            _costText.text = _upgrader.Data.Cost.ToString();
            ShowUpgradeDescription(_upgrader.GetUpgradeDescription());
            _updateReward?.Invoke();
            _soundContainer.Play(SoundType.SpendCoin);
        }
    }

    private void ShowUpgradeDescription(string description) => _upgradeDescriptionText.text = description;
}