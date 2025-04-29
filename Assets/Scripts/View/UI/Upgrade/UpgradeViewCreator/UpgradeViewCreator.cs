using System.Collections.Generic;
using UnityEngine;

public class UpgradeViewCreator : IUpgradeViewCreator
{
    private readonly UpgradeView _template;
    private readonly IEnumerable<IUpgrader> _upgraders;
    private readonly IUpgradePurchaseHandler _purchaseHandler;
    private readonly UpgradeAdvContainer _advContainer;
    private readonly Transform _container;

    public UpgradeViewCreator(UpgradeView template, IEnumerable<IUpgrader> upgraders, IUpgradePurchaseHandler purchaseHandler, UpgradeAdvContainer advContainer, Transform container)
    {
        _template = template;
        _upgraders = upgraders;
        _purchaseHandler = purchaseHandler;
        _advContainer = advContainer;
        _container = container;
    }

    public IReadOnlyCollection<IUpgradeView> CreateViews()
    {
        List<IUpgradeView> views = new List<IUpgradeView>();

        foreach (var upgrader in _upgraders)
        {
            IUpgradeView upgradeView = GameObject.Instantiate(_template, _container);
            upgradeView.Initialize(_purchaseHandler, _advContainer, upgrader);

            views.Add(upgradeView);
        }

        return views;
    }
}