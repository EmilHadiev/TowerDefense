using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class GlobalInstaller : MonoInstaller
{
    [SerializeField] private AwardGiver _awardGiver;
    [SerializeField] private LoadingScreen _loadingScreen;
    [SerializeField] private PlayerStat _playerStat;
    [SerializeField] private EnvironmentData _evnData;
    [SerializeField] private LevelTracker _levelTracker;
    [SerializeField] private TrainingData _trainingData;
    [SerializeField] private Bullet[] _bullets;
    [SerializeField] private GunData[] _gunData;
    [SerializeField] private PlayerData[] _playerData;

    public override void InstallBindings()
    {
        BindSceneSwitcher();       
        BindCoinStorage();
        BindPlayerData();
        BindEnvironmentData();
        BindLevelTrackerData();
        BindTrainingData();
        BindBullets();
        BindGuns();
        BindLoadingScreen();
        BindPurchaser();
        BindPlayers();
        BindAwardGiver();

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

    private void BindAwardGiver()
    {
        Container.Bind<AwardGiver>().FromComponentInNewPrefab(_awardGiver).AsSingle();
    }

    private void BindLoadingScreen()
    {
        Container.Bind<LoadingScreen>().FromComponentsInNewPrefab(_loadingScreen).AsSingle();
    }

    private void BindEnvironmentData()
    {
        Container.Bind<EnvironmentData>().FromScriptableObject(_evnData).AsSingle();
    }

    private void BindCoinStorage()
    {
        Container.BindInterfacesTo<CoinStorage>().AsSingle();
    }

    private void BindSceneSwitcher()
    {
        Container.BindInterfacesTo<SceneAsyncLoader>().AsSingle();
    }

    private void BindTrainingData()
    {
        Container.Bind<TrainingData>().FromNewScriptableObject(_trainingData).AsSingle();
    }

    private void BindPlayerData()
    {
        Container.Bind<PlayerStat>().FromNewScriptableObject(_playerStat).AsSingle();
    }

    private void BindLevelTrackerData()
    {
        #if UNITY_EDITOR
            Container.Bind<LevelTracker>().FromInstance(_levelTracker).AsSingle();
        #else
            Container.Bind<LevelTracker>().FromNewScriptableObject(_levelTracker).AsSingle();
        #endif
    }

    private void BindBullets()
    {
        Container.Bind<Bullet[]>().FromInstance(_bullets).AsSingle();
        Container.Bind<IBulletDefinition[]>().FromInstance(_bullets).AsSingle();
    }

    private void BindGuns()
    {
        List<GunData> guns = new List<GunData>(_gunData.Length);

        for (int i = 0; i < _gunData.Length; i++)
        {
            var gun = Instantiate(_gunData[i]);
            guns.Add(gun);
        }

        Container.Bind<GunData[]>().FromInstance(guns.ToArray()).AsSingle();
    }

    private void BindPlayers()
    {
        Container.Bind<PlayerData[]>().FromInstance(_playerData).AsSingle();
    }

    private void BindPurchaser()
    {
        Container.BindInterfacesTo<Purchaser>().AsSingle();
    }
}