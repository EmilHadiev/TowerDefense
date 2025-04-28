using System;

public interface IBulletSwitcherView
{
    event Action<string> Clicked;
    event Action<int> Used;

    void Initialize(IBulletDescription bulletData, int index, ICoinStorage coinStorage, ISoundContainer soundContainer);
}