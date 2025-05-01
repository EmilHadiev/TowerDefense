using System;

public interface IBulletsSelector
{
    int SelectBulletIndex { get; }

    event Action<int> BulletSwitched;

    void Dispose();
}