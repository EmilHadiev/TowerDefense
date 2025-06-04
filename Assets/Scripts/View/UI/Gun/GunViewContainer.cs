using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class GunViewContainer : MonoBehaviour
{
    [SerializeField] private GunView _gunView;
    [SerializeField] private Transform _container;

    private GunData[] _data;
    private IGunPlace _gunPlace;
    private IGunFactory _gunFactory;

    private readonly Dictionary<Gun, Gun> _guns = new Dictionary<Gun, Gun>(10);
    private readonly List<GunView> _views = new List<GunView>(10);

    [Inject]
    private void Constructor(GunData[] gunData, IPlayer player, IGunFactory gunFactory)
    {
        _data = gunData;
        _gunPlace = player.GunPlace;
        _gunFactory = gunFactory;
    }

    private void Awake()
    {
        CreateTemplates();
    }

    private void OnEnable()
    {
        foreach (var view in _views)
        {
            view.Selected += GunSelected;
        }
    }

    private void OnDisable()
    {
        foreach (var view in _views)
        {
            view.Selected -= GunSelected;
        }
    }

    private void CreateTemplates()
    {
        for (int i = 0; i < _data.Length; i++)
        {
            GunView view = Instantiate(_gunView, _container);
            view.Initialize(_data[i]);
            _views.Add(view);
        }
    }

    private void GunSelected(Gun gun)
    {
        Gun tmpGun = null;

        if (_guns.TryGetValue(gun, out Gun prefab))
        {
            _gunPlace.SetGun(prefab);
            tmpGun = prefab;
        }
        else
        {
            Gun newGun = CreateGun(gun);
            _guns.Add(gun, newGun);
            _gunPlace.SetGun(newGun);
            tmpGun = newGun;
        }

        SetGunData(gun, tmpGun);
    }

    private void SetGunData(Gun gun, Gun tmpGun)
    {
        tmpGun.SetData(_data.FirstOrDefault(data => data.Prefab == gun));
    }

    private Gun CreateGun(Gun prefab) => _gunFactory.Create(prefab);

    public Gun CreateAndSetAvailableGun()
    {
        GunData data = _data.FirstOrDefault(data => data.IsPurchased);
        _guns.Add(data.Prefab, CreateGun(data.Prefab));

        var gun = _guns[data.Prefab];

        gun.SetData(data);
        return gun;
    }
}
