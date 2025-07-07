using System;
using System.Collections.Generic;
using UnityEngine;

class UpgraderContainer
{
    private readonly Dictionary<Type, Upgrader> _upgraders;

    public IReadOnlyDictionary<Type, Upgrader> Upgraders => _upgraders;

    public UpgraderContainer(PlayerStat stat)
    {
        _upgraders = new Dictionary<Type, Upgrader>(3);

        Debug.Log("КОНТЕЙНЕР ДЛЯ АПГРЕЙДА ТОЖЕ НАДО ДОДЕЛАТЬ! " + nameof(UpgraderContainer));

        /*_upgraders.Add(typeof(HealthUpgrader), new HealthUpgrader(stat, GetData(UpgradeType.Health)));
        _upgraders.Add(typeof(DamageUpgrader), new DamageUpgrader(stat, GetData(UpgradeType.Damage)));
        _upgraders.Add(typeof(AttackSpeedUpgrader), new AttackSpeedUpgrader(stat, GetData(UpgradeType.AttackSpeed)));*/
    }

    public void Upgrade<T>() where T : Upgrader
    {
        if (_upgraders.TryGetValue(typeof(T), out Upgrader upgrader))
            upgrader.Upgrade();
        else
            throw new ArgumentOutOfRangeException(nameof(T));
    }
}