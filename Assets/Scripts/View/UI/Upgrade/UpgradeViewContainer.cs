using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class UpgradeViewContainer : MonoBehaviour
{
    [SerializeField] private UpgradeAdvContainer _upgradeAdvContainer;
    [SerializeField] private UpgradeView _template;
    [SerializeField] private Transform _container;

    private List<IUpgradeView> _views;
    private UpgraderContainer _upgraderContainer;
    private PlayerStat _stat;
    private IEnumerable<UpgradeData> _data;
    private IUpgradePurchaseHandler _purchaseHandler;

    private void Awake()
    {
        _views = new List<IUpgradeView>(_data.Count());
        _upgraderContainer = new UpgraderContainer(_stat, _data);
        CreateTemplates(_template);
    }

    [Inject]
    private void Constructor(PlayerStat playerStat, ICoinStorage coinStorage, ISoundContainer soundContainer, IEnumerable<UpgradeData> data)
    {
        _stat = playerStat;
        _data = data;

        _purchaseHandler = new UpgradePurchaseHandler(coinStorage, soundContainer);
    }

    private void CreateTemplates(UpgradeView template)
    {
        foreach (var upgrade in _upgraderContainer.Upgraders)
        {
            IUpgradeView upgradeView = Instantiate(template, _container);
            
            upgradeView.Initialize(_purchaseHandler, _upgradeAdvContainer, upgrade.Value);
            AddTemplate(upgradeView);
        }
    }

    private void AddTemplate(IUpgradeView upgradeView) => 
        _views.Add(upgradeView);
}