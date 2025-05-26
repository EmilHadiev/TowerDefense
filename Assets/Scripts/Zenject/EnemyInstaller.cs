using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemyInstaller : MonoInstaller
{
    [SerializeField] private EnemyStat[] _stats;
    [SerializeField] private EnemySoundContainer _soundContainer;

    public override void InstallBindings()
    {
        BindEnemyStats();
        BindEnemyUpgrader();
        BindRewardSystems();
        BindCameraProvider();
        BindEnemySoundContainer();
    }

    private void BindEnemySoundContainer()
    {
        Container.BindInterfacesTo<EnemySoundContainer>().FromComponentInNewPrefab(_soundContainer).AsSingle();
    }

    private void BindCameraProvider()
    {
        Container.BindInterfacesTo<CameraProvider>().AsSingle();
    }

    private void BindRewardSystems()
    {
        Container.Bind<RewardSystem>().AsSingle();
    }

    private void BindEnemyStats()
    {
        List<EnemyStat> stats = new List<EnemyStat>(_stats.Length);

        for (int i = 0; i < _stats.Length; i++)
        {
            var stat = Instantiate(_stats[i]);
            stats.Add(stat);
        }

        Container.Bind<IEnumerable<EnemyStat>>().FromInstance(stats).AsSingle();
        Container.Bind<IEnumerable<IEnemySound>>().FromInstance(stats).AsSingle();
    }

    private void BindEnemyUpgrader()
    {
        Container.Bind<EnemyUpgrader>().AsSingle();
    }
}