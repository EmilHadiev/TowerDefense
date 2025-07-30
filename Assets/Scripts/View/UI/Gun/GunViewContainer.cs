using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class GunViewContainer : MonoBehaviour
{
    [SerializeField] private GunView _gunView;
    [SerializeField] private Transform _container;
    [SerializeField] private ShopItemDescriptionContainer _shopItemDescriptionContainer;

    private GunData[] _data;
    private IGunPlace _gunPlace;
    private IGunFactory _gunFactory;
    private IPlayerSoundContainer _soundContainer;

    private readonly Dictionary<Gun, Gun> _guns = new Dictionary<Gun, Gun>(10);
    private readonly List<GunView> _views = new List<GunView>(10);

    [Inject]
    private void Constructor(GunData[] gunData, IPlayer player, IGunFactory gunFactory, IPlayerSoundContainer playerSoundContainer)
    {
        _data = gunData;
        _gunPlace = player.GunPlace;
        _gunFactory = gunFactory;
        _soundContainer = playerSoundContainer;
    }

    private void Awake()
    {
        CreateTemplates();
    }

    private void OnEnable()
    {
        foreach (var view in _views)
            view.Selected += GunSelected;
    }

    private void OnDisable()
    {
        foreach (var view in _views)
            view.Selected -= GunSelected;
    }

    private void CreateTemplates()
    {
        for (int i = 0; i < _data.Length; i++)
        {
            if (_data[i].IsDropped == false)
                continue;

            GunView view = Instantiate(_gunView, _container);
            view.Initialize(_data[i], _shopItemDescriptionContainer);
            _views.Add(view);

            if (i == 0)
                view.BackgroundToggle(true);
        }
    }

    private void GunSelected(Gun gun)
    {
        Gun tmpGun;

        if (_guns.TryGetValue(gun, out Gun prefab))
        {
            tmpGun = prefab;
        }
        else
        {
            Gun newGun = CreateGun(gun);
            _guns.Add(gun, newGun);
            tmpGun = newGun;
        }

        SetGunData(gun, tmpGun);
        _gunPlace.SetGun(tmpGun);
        HideViewBackground();
    }

    private void SetGunData(Gun gun, Gun tmpGun)
    {
        tmpGun.SetData(_data.FirstOrDefault(data => data.Prefab == gun));
    }

    private Gun CreateGun(Gun prefab) => _gunFactory.Create(prefab);

    public Gun CreateAndSetAvailableGun()
    {
        GunData data = _data.FirstOrDefault(data => data.IsDropped);
        _guns.Add(data.Prefab, CreateGun(data.Prefab));

        var gun = _guns[data.Prefab];

        gun.SetData(data);
        return gun;
    }

    private void HideViewBackground()
    {
        foreach (var view in _views)
            view.BackgroundToggle(false);
    }
}