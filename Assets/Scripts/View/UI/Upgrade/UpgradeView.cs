using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(UpgradeViewRender))]
public class UpgradeView : MonoBehaviour, IUpgradeView
{
    [SerializeField] private UpgradeViewRender _render;
    [SerializeField] private Button _buyButton;

    private IUpgradePurchaseHandler _upgradePurchaseHandler;
    private IRewardUpdateCommand _updateCommand;
    private IUpgrader _upgrader;

    private void OnValidate()
    {
        _render ??= GetComponent<UpgradeViewRender>();
    }

    private void OnEnable()
    {
        _buyButton.onClick.AddListener(OnTryBuyUpgrade);
    }

    private void OnDisable()
    {
        _buyButton.onClick.RemoveListener(OnTryBuyUpgrade);
    }

    public void Initialize(IUpgradePurchaseHandler purchaseHandler, IRewardUpdateCommand updateCommand, IUpgrader upgrader)
    {
        _upgradePurchaseHandler = purchaseHandler;
        _updateCommand = updateCommand;
        _upgrader = upgrader;

        _render.Initialize(_upgrader);
        Show();
    }

    private void Show()
    {
        ShowUpgradeDescription();
        UpdatePrice();
    }

    private void OnTryBuyUpgrade()
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
        ShowUpgradeDescription();
    }

    private void Upgrade() => _upgrader.Upgrade();

    private void ShowUpgradeDescription() => _render.UpdateDescription();

    private void UpdatePrice() => _render.UpdatePrice();
}