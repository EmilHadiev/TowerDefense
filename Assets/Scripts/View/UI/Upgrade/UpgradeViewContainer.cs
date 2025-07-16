using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class UpgradeViewContainer : MonoBehaviour
{
    [SerializeField] private UpgradeView _template;
    [SerializeField] private Transform _container;

    private IUpgradeViewCreator _creator;

    private void Awake()
    {        
        CreateTemplates();
    }

    [Inject]
    private void Constructor(ICoinStorage coinStorage, IPlayerSoundContainer soundContainer, GunData[] gunData, IPlayer player)
    {
        _creator = new UpgradeViewCreator(_template, GetGunData(gunData), coinStorage, soundContainer, _container, player.GunPlace);
    }

    private void CreateTemplates()
    {
        _creator.CreateViews();
    }

    private IEnumerable<GunData> GetGunData(GunData[] gunData)
    {
        return gunData.Where(gun => gun.IsDropped);
    }        
}