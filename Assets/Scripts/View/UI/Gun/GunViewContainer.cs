using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GunViewContainer : MonoBehaviour
{
    [SerializeField] private GunView _gunView;
    [SerializeField] private Transform _container;

    private GunData[] _data;
    private IGunPlace _gunPlace;

    private readonly Dictionary<Gun, Gun> _guns = new Dictionary<Gun, Gun>(10);
    private readonly List<GunView> _views = new List<GunView>(10);

    [Inject]
    private void Constructor(GunData[] gunData, IPlayer player)
    {
        _data = gunData;
        _gunPlace = player.GunPlace;
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
        if (_guns.TryGetValue(gun, out Gun prefab) == false)
        {

        }

        _gunPlace.SetGun(gun);
    }
}
