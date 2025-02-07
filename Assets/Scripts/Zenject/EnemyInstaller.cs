using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemyInstaller : MonoInstaller
{
    [SerializeField] private EnemyStat[] _stats;

    public override void InstallBindings()
    {
        BindEnemyStats();
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
    }
}