using UnityEngine;
using Zenject;

public class LevelInstaller : MonoInstaller
{
    [SerializeField] private LevelEntryPoint _levelPoint;

    public override void InstallBindings()
    {
        BindLevelEntryPoint();
        BindEnemyCounter();
    }

    private void BindEnemyCounter()
    {
        Container.Bind<EnemyCounter>().AsSingle();
    }

    private void BindLevelEntryPoint()
    {
        Container.BindInterfacesTo<LevelEntryPoint>().FromInstance(_levelPoint).AsSingle();
    }
}