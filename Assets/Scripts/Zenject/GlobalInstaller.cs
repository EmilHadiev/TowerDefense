using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GlobalInstaller : MonoInstaller
{
    [SerializeField] private CoroutinePerformer _performer;
    [SerializeField] private UpgradeData[] _data;
    [SerializeField] private PlayerStat _playerStat;

    public override void InstallBindings()
    {
        BindCoroutinePerformer();
        BindSceneSwitcher();
        BindAdvertising();
        BindSave();
        BindCoinStorage();
        BindUpgradeData();
        BindPlayerData();
        BindGameplayMarkup();
        BindPause();
    }

    private void BindPause()
    {
        Container.Bind<Pause>().AsSingle();
    }

    private void BindGameplayMarkup()
    {
        Container.Bind<GameplayMarkup>().AsSingle();
    }

    private void BindUpgradeData()
    {
        List<UpgradeData> data = new List<UpgradeData>(_data.Length);

        for (int i = 0; i < _data.Length; i++)
        {
            var stat = Instantiate(_data[i]);
            data.Add(_data[i]);
        }

        Container.Bind<IEnumerable<UpgradeData>>().FromInstance(_data);
    }

    private void BindSave()
    {
        Container.BindInterfacesTo<YandexSaver>().AsSingle().NonLazy();
    }

    private void BindCoinStorage()
    {
        Container.BindInterfacesTo<CoinStorage>().AsSingle();
    }

    private void BindSceneSwitcher()
    {
        Container.Bind<SceneSwitcher>().AsSingle();
    }

    private void BindCoroutinePerformer()
    {
        Container.BindInterfacesTo<CoroutinePerformer>().FromComponentInNewPrefab(_performer).AsSingle();
    }

    private void BindPlayerData()
    {
        Container.Bind<PlayerStat>().FromNewScriptableObject(_playerStat).AsSingle();
    }

    private void BindAdvertising()
    {
        Container.BindInterfacesTo<YandexAdv>().AsSingle();
    }
}