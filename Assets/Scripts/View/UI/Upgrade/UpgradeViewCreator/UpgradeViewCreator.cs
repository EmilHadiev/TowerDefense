using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UpgradeViewCreator : IUpgradeViewCreator
{
    private readonly UpgradeView _template;
    private readonly GunData[] _gunData;
    private readonly ICoinStorage _coinStorage;
    private readonly Transform _container;
    private readonly IPlayerSoundContainer _soundContainer;
    private readonly List<UpgradeView> _views;
    private readonly IGunPlace _gunPlace;

    public UpgradeViewCreator(UpgradeView template, IEnumerable<GunData> gunData, ICoinStorage coinStorage, 
        IPlayerSoundContainer soundContainer, Transform container, IGunPlace gunPlace)
    {
        _gunPlace = gunPlace;
        _template = template;        
        _coinStorage = coinStorage;
        _soundContainer = soundContainer;
        _container = container;

        _gunData = gunData.ToArray();
        _views = new List<UpgradeView>(_gunData.Length);
        InitViews();
    }

    private void InitViews()
    {
        for (int i = 0; i < _gunData.Length; i++)
        {
            UpgradeView upgradeView = GameObject.Instantiate(_template, _container);
            upgradeView.Initialize(_coinStorage, _gunData[i], _soundContainer, _gunPlace);
            _views.Add(upgradeView);
        }
    }

    public IReadOnlyCollection<UpgradeView> CreateViews()
    {
        return _views;
    }
}