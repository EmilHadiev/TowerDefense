using UnityEngine;
using Zenject;

public class EnemyInstaller : MonoInstaller
{
    [SerializeField] private SkeletonStat _skeletonStat;
    [SerializeField] private DemonKnightStat _demonKnightStat;
    [SerializeField] private GolemStat _golemStat;
    [SerializeField] private BlackKnightStat _blackKnightStat;
    [SerializeField] private ArmorKnightStat _armorKnightStat;
    [SerializeField] private TurtleStat _turtleStat;
    [SerializeField] private DragonStat _dragonStat;
    [SerializeField] private SlimeStat _slimeStat;

    public override void InstallBindings()
    {
        BindEnemyStats();
    }

    private void BindEnemyStats()
    {
        Container.Bind<EnemyStat>().To<SkeletonStat>().FromNewScriptableObject(_skeletonStat).AsSingle();
        Container.Bind<EnemyStat>().To<DemonKnightStat>().FromNewScriptableObject(_demonKnightStat).AsSingle();
        Container.Bind<EnemyStat>().To<GolemStat>().FromNewScriptableObject(_golemStat).AsSingle();
        Container.Bind<EnemyStat>().To<BlackKnightStat>().FromNewScriptableObject(_blackKnightStat).AsSingle();
        Container.Bind<EnemyStat>().To<ArmorKnightStat>().FromNewScriptableObject(_armorKnightStat).AsSingle();
        Container.Bind<EnemyStat>().To<TurtleStat>().FromNewScriptableObject(_turtleStat).AsSingle();
        Container.Bind<EnemyStat>().To<DragonStat>().FromNewScriptableObject(_dragonStat).AsSingle();
        Container.Bind<EnemyStat>().To<SlimeStat>().FromNewScriptableObject(_slimeStat).AsSingle();
    }
}