using System;
using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private Player _player;
    [SerializeField] private SoundContainer _soundContainer;
    [SerializeField] private PlayerStat _playerStat;
    [SerializeField] private bool _isDesktop;

    public override void InstallBindings()
    {
        BindAdvertising();
        BindPlayer();
        BindInput();
        BindPlayerAttacker();
        BindPlayerData();
        BindCoinStorage();
        BindSoundContainer();
        BindBulletContainer();
    }

    private void BindAdvertising()
    {
        Container.BindInterfacesTo<YandexAdv>().AsSingle();
    }

    private void BindBulletContainer()
    {
        Container.BindInterfacesAndSelfTo<BulletDataUpgraderContainer>().AsSingle();
    }

    private void BindSoundContainer()
    {
        Container.BindInterfacesTo<SoundContainer>().FromComponentInNewPrefab(_soundContainer).AsSingle();
    }

    private void BindCoinStorage()
    {
        Container.BindInterfacesTo<CoinStorage>().AsSingle();
    }

    private void BindPlayerData()
    {
        Container.Bind<PlayerStat>().FromNewScriptableObject(_playerStat).AsSingle();
    }

    private void BindPlayerAttacker()
    {
        Container.BindInterfacesTo<PlayerAttacker>().AsSingle().NonLazy();
    }

    private void BindPlayer()
    {
        Container.BindInterfacesTo<Player>().FromInstance(_player).AsSingle();
    }

    private void BindInput()
    {
        if (_isDesktop)
            Container.BindInterfacesTo<DesktopInput>().AsSingle();
    }
}