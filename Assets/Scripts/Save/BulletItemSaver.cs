﻿using System.Collections.Generic;
using System.Linq;

class BulletItemSaver
{
    private readonly IBulletDefinition[] _bullets;
    private readonly List<BulletItem> _items;

    public BulletItemSaver(IBulletDefinition[] bullets, List<BulletItem> items)
    {
        _bullets = bullets;
        _items = items;
        Init();
    }

    public void Save()
    {
        for (int i = 0; i < _items.Count; i++)
            UpdateAvailable(_items[i], _bullets[i]);
    }

    private void Init()
    {
        if (_items.Count() == 0)
            FirstInit();
        else
            DefaultInit();
    }

    private void DefaultInit()
    {
        for (int i = 0; i < _bullets.Length; i++)
            _bullets[i].BulletDescription.IsDropped = _items[i].IsDropped;
    }

    private void FirstInit()
    {
        for (int i = 0; i < _bullets.Length; i++)
        {
            var item = new BulletItem
            {
                Type = _bullets[i].Type,
                IsDropped = _bullets[i].BulletDescription.IsDropped
            };

            _items.Add(item);
        }
    }

    private void UpdateAvailable(BulletItem item, IBulletDefinition bullet) => item.IsDropped = bullet.BulletDescription.IsDropped;
}