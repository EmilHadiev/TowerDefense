using System.Collections.Generic;

public class EnemyData
{
    private readonly List<EnemyStat> _stats;

    public EnemyData(IEnumerable<EnemyStat> stats)
    {
        _stats = new List<EnemyStat>(stats);
    }

    public bool TryGetStat(EnemyType enemy, EnemyStat stat)
    {
        for (int i = 0; i < _stats.Count; i++)
        {
            if (_stats[i].EnemyType == enemy)
            {
                stat = _stats[i];
                return true;
            }
        }

        stat = null;
        return false;
    }
}