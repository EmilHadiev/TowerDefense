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
        Debug.Log("œŒ ¿ ◊“Œ —À”◊¿…Õ€… »√–Œ !");
        //Container.BindInterfacesTo<Player>().FromComponentInNewPrefab(_player).AsSingle();

        string[] players =
        {
            "Players/CrazyDoctor",
            "Players/Ghost",
            "Players/King",
            "Players/Knight",
            "Players/Mummy",
            "Players/Pirate",
            "Players/Queen",
            "Players/Maid",
            "Players/Witch",
        };

        int randomIndex = UnityEngine.Random.Range(0, players.Length);

        Container.BindInterfacesTo<Player>().FromComponentInNewPrefabResource(players[randomIndex]).AsSingle();
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
            Container.BindInterfacesTo<MobileInput>().AsSingle();
        }
    }
}