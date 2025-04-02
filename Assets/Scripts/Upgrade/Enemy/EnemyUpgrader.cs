using System.Collections.Generic;
using System.Linq;
using UnityEngine;

class EnemyUpgrader
{
    private const int ImprovementFactor = 10;

    private readonly IEnumerable<EnemyStat> _stats;
    private readonly EnemyLevelData _levelData;
    private readonly Dictionary<EnemyType, EnemyData> _data;

    public EnemyUpgrader(IEnumerable<EnemyStat> stat, EnemyLevelData level)
    {
        _stats = stat;
        _levelData = level;

        _data = new Dictionary<EnemyType, EnemyData>(stat.Count());
        InitData();
    }

    public void TryUpgrade()
    {
        foreach (var stat in _stats)
        {
            if (_data.TryGetValue(stat.EnemyType, out EnemyData data))
            {
                stat.Health = data.Health;
                stat.Damage = data.Damage;
            }
        }

        Debug.Log($"Текущий уровень улучшения: {_levelData.Level}");
    }

    public void LevelUp() => _levelData.Level++;

    private void InitData()
    {
        foreach (var enemyStat in _stats)
            _data.Add(enemyStat.EnemyType, new EnemyData(_levelData, enemyStat));
    }

    private struct EnemyData
    {
        private readonly EnemyLevelData _levelData;

        private readonly float _startHealth;
        private readonly float _startDamage;

        public float Damage => _startDamage + _levelData.Level;
        public float Health => _startHealth + (_levelData.Level * ImprovementFactor);

        public EnemyData(EnemyLevelData data, EnemyStat stat)
        {
            _levelData = data;

            _startHealth = stat.Health;
            _startDamage = stat.Damage;
        }
    }
}
