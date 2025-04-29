using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class UpgradeViewContainer : MonoBehaviour
{
    [SerializeField] private UpgradeAdvContainer _upgradeAdvContainer;
    [SerializeField] private UpgradeView _template;
    [SerializeField] private Transform _container;

    private IUpgradeViewCreator _creator;

    private void Awake()
    {        
        CreateTemplates();
    }

    [Inject]
    private void Constructor(PlayerStat playerStat, ICoinStorage coinStorage, ISoundContainer soundContainer, IEnumerable<UpgradeData> data)
    {
        UpgraderContainer upgraderContainer = new UpgraderContainer(playerStat, data);
        IUpgradePurchaseHandler purchaseHandler = new UpgradePurchaseHandler(coinStorage, soundContainer);

        _creator = new UpgradeViewCreator(_template, GetUpgraders(upgraderContainer), purchaseHandler, _upgradeAdvContainer, _container);
    }

    private void CreateTemplates()
    {
        _creator.CreateViews();
    }

    private IEnumerable<IUpgrader> GetUpgraders(UpgraderContainer container) => 
        container.Upgraders.Select(upgrader => upgrader.Value);
}