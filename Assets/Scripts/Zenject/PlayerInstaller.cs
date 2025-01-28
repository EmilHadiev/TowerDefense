using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private Player _player;
    [SerializeField] private PlayerStat _playerStat;
    [SerializeField] private bool _isDesktop;

    public override void InstallBindings()
    {
        BindPlayer();
        BindInput();
        BindPlayerAttacker();
        BindPlayerData();
    }

    private void BindPlayerData()
    {
        Container.Bind<PlayerStat>().FromScriptableObject(_playerStat).AsSingle();
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