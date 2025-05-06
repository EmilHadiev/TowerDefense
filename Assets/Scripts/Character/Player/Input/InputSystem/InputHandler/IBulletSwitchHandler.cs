using System;

public interface IBulletSwitchHandler
{
    event Action<int> SwitchBulletButtonClicked;

    void SwitchTo(int bulletIndex);
}