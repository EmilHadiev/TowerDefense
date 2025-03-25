using System;

public interface IInputSystem
{
    event Action<int> SwitchBulletButtonClicked;

    void SwitchTo(int bulletIndex);
}