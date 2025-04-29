using System;
using UnityEngine;

public abstract class Pause
{
    public event Action Started;
    public event Action Exited;

    protected readonly IAdvertising Advertising;

    public Pause(IAdvertising advertising)
    {
        Advertising = advertising;
    }

    public virtual void Continue()
    {
        Time.timeScale = 1;
        Started?.Invoke();
    }

    public virtual void Stop()
    {
        Time.timeScale = 0;
        Advertising.ShowInterstitialAdv();
        Exited?.Invoke();
    }
}