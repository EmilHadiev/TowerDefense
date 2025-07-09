using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UpgradeViewContainer : MonoBehaviour
{
    [SerializeField] private UpgradeView _template;
    [SerializeField] private Transform _container;
    [SerializeField] private ScrollRect _rect; 

    private IUpgradeViewCreator _creator;

    private void Awake()
    {        
        CreateTemplates();
    }

    [Inject]
    private void Constructor(ICoinStorage coinStorage, IPlayerSoundContainer soundContainer, GunData[] gunData)
    {
        _creator = new UpgradeViewCreator(_template, GetGunData(gunData), coinStorage, soundContainer, _container);
        Debug.Log("Надо доделать " + nameof(UpgradeViewContainer));
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