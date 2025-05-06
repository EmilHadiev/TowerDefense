using System;

public class BulletsSelector : IDisposable, IBulletsSelector
{
    private readonly IBulletSwitchHandler _input;
    private readonly Bullet[] _bullets;

    public event Action<int> BulletSwitched;

    public BulletsSelector(IBulletSwitchHandler input, Bullet[] bulletTemplates)
    {
        _input = input;
        _bullets = bulletTemplates;
        _input.SwitchBulletButtonClicked += OnBulletSwitched;
    }

    public int SelectBulletIndex { get; private set; }

    public void Dispose() => _input.SwitchBulletButtonClicked -= OnBulletSwitched;

    private void OnBulletSwitched(int index)
    {
        SelectBulletIndex = GetIAvailableIndex(index);

        if (SelectBulletIndex == -1)
            throw new ArgumentOutOfRangeException(nameof(index));

        BulletSwitched?.Invoke(SelectBulletIndex);
    }

    private int GetIAvailableIndex(int bulletIndex)
    {
        int index = -1;

        if (bulletIndex < 0 || bulletIndex >= _bullets.Length)
            index = -1;
        else
            index = bulletIndex;

        if (IsPurchasedBullet(SelectBulletIndex) == false || SelectBulletIndex == -1)
            index = GetPurchasedIndex();

        return index;
    }

    private bool IsPurchasedBullet(int bulletIndex) => _bullets[bulletIndex].BulletDescription.IsPurchased;

    private int GetPurchasedIndex()
    {
        for (int i = 0; i < _bullets.Length; i++)
            if (_bullets[i].BulletDescription.IsPurchased)
                return i;

        throw new ArgumentOutOfRangeException(nameof(_bullets));
    }
}