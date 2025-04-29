using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeView : MonoBehaviour, IUpgradeView
{
    [SerializeField] private Image _upgradeImage;
    [SerializeField] private TMP_Text _upgradeNameText;
    [SerializeField] private TMP_Text _upgradeDescriptionText;
    [SerializeField] private TMP_Text _costText;
    [SerializeField] private Button _buyButton;

    private IUpgradePurchaseHandler _upgradePurchaseHandler;
    private IRewardUpdateCommand _updateCommand;
    private IUpgrader _upgrader;

    private void OnEnable()
    {
        _buyButton.onClick.AddListener(OnClicked);
    }

    private void OnDisable()
    {
        _buyButton.onClick.RemoveListener(OnClicked);
    }

    public void Initialize(IUpgradePurchaseHandler purchaseHandler, IRewardUpdateCommand updateCommand, IUpgrader upgrader)
    {
        _upgradePurchaseHandler = purchaseHandler;
        _updateCommand = updateCommand;
        _upgrader = upgrader;
        Show();
    }

    private void Show()
    {
        _upgradeImage.sprite = _upgrader.Data.Sprite;
        _upgradeNameText.text = _upgrader.Data.Name;
        ShowUpgradeDescription(_upgrader.GetUpgradeDescription());
        UpdatePrice();
    }

    private void OnClicked()
    {
        if (_upgradePurchaseHandler.TryUpgrade(_upgrader.Data))
        {
            Upgrade();
            UpdateDescription();
        }
    }

    private void UpdateDescription()
    {
        _updateCommand.UpdateReward();
        UpdatePrice();
        ShowUpgradeDescription(_upgrader.GetUpgradeDescription());
    }

    private void Upgrade() => _upgrader.Upgrade();

    private void ShowUpgradeDescription(string description) => _upgradeDescriptionText.text = description;

    private void UpdatePrice() => _costText.text = _upgrader.Data.Cost.ToString();
}