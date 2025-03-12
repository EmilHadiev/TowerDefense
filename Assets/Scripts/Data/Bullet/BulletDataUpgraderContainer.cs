using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

class BulletDataUpgraderContainer : IInitializable, IDisposable
{
    private readonly List<BulletData> _bulletsData;

    public BulletDataUpgraderContainer(PlayerStat playerStat)
    {
        _bulletsData = new List<BulletData>(10);
        Debug.Log("Bullets");
    }

    public void Initialize()
    {

    }

    public void Dispose()
    {
        Debug.Log("Dispose - но этот класс надо удалить ...");
    }

    public void Add(BulletData data) => _bulletsData.Add(data);
}