using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BulletsSelector : IDisposable, IBulletsSelector
{
    private readonly IBulletSwitchHandler _input;
    private readonly Bullet[] _bullets;

    public event Action<int> BulletSwitched;

    public BulletsSelector(IBulletSwitchHandler input, IEnumerable<Bullet> availableBullets)
    {
        _input = input;
        _bullets = availableBullets.ToArray();
        Debug.Log(_bullets.Length + " размер!");
        _input.SwitchBulletButtonClicked += OnBulletSwitched;
    }

    public int SelectBulletIndex { get; private set; }

    public void Dispose() => _input.SwitchBulletButtonClicked -= OnBulletSwitched;

    private void OnBulletSwitched(int index)
    {
        SelectBulletIndex = GetIAvailableIndex(index);

        if (SelectBulletIndex == -1)
            throw new ArgumentOutOfRangeException(nameof(index));

        Debug.Log("Выбранный индекс - " + SelectBulletIndex);
        BulletSwitched?.Invoke(SelectBulletIndex);
    }

    private int GetIAvailableIndex(int bulletIndex)
    {
        int index = -1;

        if (bulletIndex < 0 || bulletIndex >= _bullets.Length || IsDropped(SelectBulletIndex) == false)
            index = GetDroppedIndex();
        else
            index = bulletIndex;

        return index;
    }

    private bool IsDropped(int bulletIndex) => _bullets[bulletIndex].BulletDescription.IsDropped;

    private int GetDroppedIndex()
    {
        for (int i = 0; i < _bullets.Length; i++)
            if (_bullets[i].BulletDescription.IsDropped)
                return i;

        throw new ArgumentOutOfRangeException(nameof(_bullets));
    }
}