using UnityEngine;
using Zenject;

public class EnemyInstaller : MonoInstaller
{
    [SerializeField] private SkeletonStat _skeletonStat;

    public override void InstallBindings()
    {
        BindEnemyStats();
    }

    private void BindEnemyStats()
    {
        Container.Bind<SkeletonStat>().FromNewScriptableObject(_skeletonStat).AsSingle();
    }
}