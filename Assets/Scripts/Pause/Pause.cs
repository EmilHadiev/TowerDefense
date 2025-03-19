using UnityEngine;

public abstract class Pause
{
    protected readonly IAdvertising Advertising;

    public Pause(IAdvertising advertising)
    {
        Advertising = advertising;
    }

    public virtual void Start() => Time.timeScale = 1;

    public virtual void Stop()
    {
        Time.timeScale = 0;
        Advertising.ShowInterstitialAdv();
    }
}