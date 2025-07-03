using System;

public interface IBulletView
{
    event Action<string> Clicked;
    event Action<int> Used;

    void Initialize(IBulletDescription bulletDescription, int index, IPlayerSoundContainer soundContainer);
}