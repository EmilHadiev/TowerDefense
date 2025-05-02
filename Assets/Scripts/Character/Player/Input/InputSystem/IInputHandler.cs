using System;

public interface IInputHandler
{
    event Action<int> SwitchBulletButtonClicked;

    void SwitchTo(int bulletIndex);
}