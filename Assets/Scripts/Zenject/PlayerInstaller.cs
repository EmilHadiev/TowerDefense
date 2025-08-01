using System;
using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private Player _player;
    [SerializeField] private PlayerSoundContainer _soundContainer;
    [SerializeField] private EnvironmentData _envData;

    public override void InstallBindings()
    {        
        BindInput();
        BindPlayerAttacker();
        BindSoundContainer();
        BindOptimization();
        BindPlayer();
        BindProfits();
        BindHealthUpgrader();
    }

    private void BindHealthUpgrader()
    {
        Container.Bind<HealthUpgrader>().AsSingle();
    }

    private void BindProfits()
    {
        Container.BindInterfacesAndSelfTo<ProfitContainer>().AsSingle();
    }

    private void BindOptimization()
    {
        Container.BindInterfacesAndSelfTo<Optimizator>().AsSingle();
    }

    private void BindSoundContainer()
    {
        Container.BindInterfacesTo<PlayerSoundContainer>().FromComponentInNewPrefab(_soundContainer).AsSingle();
    }

    private void BindPlayerAttacker()
    {
        Container.BindInterfacesTo<PlayerAttacker>().AsSingle();
    }

    private void BindPlayer()
    {
        Container.BindInterfacesTo<Player>().FromComponentInNewPrefab(_envData.PlayerData.Prefab).AsSingle();
    }

    private void BindInput()
    {
        Container.BindInterfacesTo<PlayerInputSystem>().AsSingle();
        Container.BindInterfacesTo<BulletSwitchHandler>().AsSingle();

        if (_envData.IsDesktop)
        {
            Container.BindInterfacesTo<DesktopPlayerRotator>().AsSingle();
            Container.BindInterfacesTo<DesktopMoveHandler>().AsSingle();
            Container.BindInterfacesTo<DesktopInput>().AsSingle();
        }
        else
        {
            Container.BindInterfacesTo<MobilePlayerRotator>().AsSingle();
            Container.BindInterfacesTo<JoystickFactory>().AsSingle();
            BindOnlyAttack();
        }
    }

    private void BindOnlyAttack() => Container.BindInterfacesTo<SimpleMobileInput>().AsSingle();

    private void BindAttackAndMove() => Container.BindInterfacesAndSelfTo<FullMobileInput>().AsSingle();
}