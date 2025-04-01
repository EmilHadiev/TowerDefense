using System.Collections.Generic;
using System.Linq;
using UnityEngine;

class EnemyUpgrader
{
    private const int ImprovementFactor = 15;

    private readonly IEnumerable<EnemyStat> _stats;
    private readonly EnemyLevelData _level;
    private readonly Dictionary<EnemyType, EnemyData> _data;

    public EnemyUpgrader(IEnumerable<EnemyStat> stat, EnemyLevelData level)
    {
        _stats = stat;
        _level = level;

        _data = new Dictionary<EnemyType, EnemyData>(stat.Count());
        InitData();
    }

    public void Upgrade()
    {
        foreach (var stat in _stats)
        {
            Debug.Log($"{stat.EnemyType} урон: {stat.Damage}, здоровье {stat.Health}");

            foreach (var data in _data)
            {                
                stat.Health = data.Value.Health();
                stat.Damage = data.Value.Damage();                
            }

            Debug.Log($"{stat.EnemyType} урон: {stat.Damage}, здоровье {stat.Health}");
            Debug.Log(new string('-', 20));
        }
    }

    public void LevelUp() => _level.Level++;

    private void InitData()
    {
        foreach (var enemyStat in _stats)
            _data.Add(enemyStat.EnemyType, new EnemyData(_level, enemyStat.Health, enemyStat.Damage));
    }

    private struct EnemyData
    {
        private readonly EnemyLevelData _data;

        private readonly float _startHealth;
        private readonly float _startDamage;

        public float Damage() => _startDamage + _data.Level;
        public float Health() => _startHealth + (_data.Level * ImprovementFactor);

        public EnemyData(EnemyLevelData data, float startHealth, float startDamage)
        {
            _data = data;

            _startHealth = startHealth;
            _startDamage = startHealth;
        }
    }
}
