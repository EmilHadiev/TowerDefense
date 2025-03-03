using System;

public interface IDesktopInput
{
    event Action<int> SwitchBulletButtonClicked;
}