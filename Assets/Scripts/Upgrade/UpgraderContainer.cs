using System;
using System.Collections.Generic;
using System.Linq;

class UpgraderContainer
{
    private readonly Dictionary<Type, Upgrader> _upgraders;
    private readonly IEnumerable<UpgradeData> _data;

    public IReadOnlyDictionary<Type, Upgrader> Upgraders => _upgraders;

    public UpgraderContainer(PlayerStat stat, IEnumerable<UpgradeData> data)
    {
        _upgraders = new Dictionary<Type, Upgrader>(3);
        _data = data;

        _upgraders.Add(typeof(HealthUpgrader), new HealthUpgrader(stat, GetData(UpgradeType.Health)));
        _upgraders.Add(typeof(DamageUpgrader), new DamageUpgrader(stat, GetData(UpgradeType.Damage)));
        _upgraders.Add(typeof(AttackSpeedUpgrader), new AttackSpeedUpgrader(stat, GetData(UpgradeType.AttackSpeed)));
    }

    public void Upgrade<T>() where T : Upgrader
    {
        if (_upgraders.TryGetValue(typeof(T), out Upgrader upgrader))
            upgrader.Upgrade();
        else
            throw new ArgumentOutOfRangeException(nameof(T));
    }

    private UpgradeData GetData(UpgradeType type)
    {
        UpgradeData data = _data.FirstOrDefault(data => data.UpgradeType == type);

        if (data is null)
            throw new ArgumentNullException(nameof(type));

        return data;
    }
}