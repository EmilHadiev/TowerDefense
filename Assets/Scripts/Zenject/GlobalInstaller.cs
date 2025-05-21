using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GlobalInstaller : MonoInstaller
{
    [SerializeField] private CoroutinePerformer _performer;
    [SerializeField] private LoadingScreen _loadingScreen;
    [SerializeField] private UpgradeData[] _data;
    [SerializeField] private PlayerStat _playerStat;
    [SerializeField] private EnvironmentData _evnData;
    [SerializeField] private EnemyLevelData _enemyLevel;
    [SerializeField] private Bullet[] _bullets;

    public override void InstallBindings()
    {
        BindCoroutinePerformer();
        BindSceneSwitcher();       
        BindCoinStorage();
        BindUpgradeData();
        BindPlayerData();
        BindEnvironmentData();
        BindEnemyData();
        BindBullets();
        BindLoadingScreen();

        #region DefferentPlatforms
        BindAdvertising();
        BindSave();
        BindPause();
        BindGameplayMarkup();
        #endregion
    }

    #region DefferentPlatforms
    private void BindPause()
    {
        #if UNITY_WEBGL
            Container.Bind<Pause>().To<YandexPause>().AsSingle();
        #endif
    }

    private void BindGameplayMarkup()
    {
        #if UNITY_WEBGL
            Container.Bind<GameplayMarkup>().AsSingle();
        #endif
    }

    private void BindSave()
    {
        #if UNITY_WEBGL
            Container.BindInterfacesTo<YandexSaver>().AsSingle().NonLazy();
        #endif
    }

    private void BindAdvertising()
    {
        #if UNITY_WEBGL
            Container.BindInterfacesTo<YandexAdv>().AsSingle();
        #endif
    }
    #endregion

    private void BindLoadingScreen()
    {
        Container.Bind<LoadingScreen>().FromComponentsInNewPrefab(_loadingScreen).AsSingle();
    }

    private void BindEnvironmentData()
    {
        Container.Bind<EnvironmentData>().FromScriptableObject(_evnData).AsSingle();
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

    private void BindCoinStorage()
    {
        Container.BindInterfacesTo<CoinStorage>().AsSingle();
    }

    private void BindSceneSwitcher()
    {
        Container.BindInterfacesTo<SceneLoader>().AsSingle();
    }

    private void BindCoroutinePerformer()
    {
        Container.BindInterfacesTo<CoroutinePerformer>().FromComponentInNewPrefab(_performer).AsSingle();
    }

    private void BindPlayerData()
    {
        Container.Bind<PlayerStat>().FromNewScriptableObject(_playerStat).AsSingle();
    }

    private void BindEnemyData()
    {
        Container.Bind<EnemyLevelData>().FromNewScriptableObject(_enemyLevel).AsSingle();
    }

    private void BindBullets()
    {
        Container.Bind<Bullet[]>().FromInstance(_bullets);
        Container.Bind<IBulletDefinition[]>().FromInstance(_bullets);
    }
}