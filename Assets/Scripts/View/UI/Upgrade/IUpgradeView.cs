public interface IUpgradeView
{
    void Initialize(IUpgradePurchaseHandler purchaseHandler, IRewardUpdateCommand updateCommand, IUpgrader upgrader);
}