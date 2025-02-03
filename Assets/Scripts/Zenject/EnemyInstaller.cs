using UnityEngine;
using Zenject;

public class EnemyInstaller : MonoInstaller
{
    [SerializeField] private SkeletonStat _skeletonStat;
    [SerializeField] private DemonKnightStat _demonKnightStat;
    [SerializeField] private EnemyStat[] _stats;

    public override void InstallBindings()
    {
        BindEnemyStats();
    }

    private void BindEnemyStats()
    {
        Container.Bind<EnemyStat>().To<SkeletonStat>().FromNewScriptableObject(_skeletonStat).AsSingle();
        Container.Bind<EnemyStat>().To<DemonKnightStat>().FromNewScriptableObject(_demonKnightStat).AsSingle();
    }
}