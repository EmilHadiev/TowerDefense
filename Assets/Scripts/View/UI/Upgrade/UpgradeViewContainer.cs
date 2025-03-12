using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class UpgradeViewContainer : MonoBehaviour
{
    [SerializeField] private UpgradeView _template;
    [SerializeField] private Transform _container;
    //[SerializeField] private UpgradeData[] _data;

    private List<UpgradeView> _views;
    private UpgraderContainer _upgraderContainer;
    private PlayerStat _stat;
    private ICoinStorage _coinStorage;
    private ISoundContainer _soundContainer;
    private IEnumerable<UpgradeData> _data;

    private void Awake()
    {
        _views = new List<UpgradeView>(_data.Count());
        _upgraderContainer = new UpgraderContainer(_stat, _data);
        CreateTemplates(_template);
    }

    [Inject]
    private void Constructor(PlayerStat playerStat, ICoinStorage coinStorage, ISoundContainer soundContainer, IEnumerable<UpgradeData> data)
    {
        _stat = playerStat;
        _coinStorage = coinStorage;
        _soundContainer = soundContainer;
        _data = data;
    }

    private void CreateTemplates(UpgradeView template)
    {
        foreach (var upgrade in _upgraderContainer.Upgraders)
        {
            UpgradeView upgradeView = Instantiate(template, _container);
            upgradeView.Initialize(_coinStorage, _soundContainer, upgrade.Value);
            _views.Add(upgradeView);
        }
    }
}