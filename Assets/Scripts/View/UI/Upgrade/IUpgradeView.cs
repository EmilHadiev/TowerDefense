public interface IUpgradeView
{
    void Initialize(IUpgradePurchaseHandler purchaseHandler, IRewardUpdateCommand updateCommand, Upgrader upgrader);
}