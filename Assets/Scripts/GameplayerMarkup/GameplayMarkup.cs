using System;
using YG;

public class GameplayMarkup
{
    public void Start() => YG2.GameplayStart();

    public void Stop() => YG2.GameplayStop();

    public void Ready() => YG2.GameReadyAPI();
}