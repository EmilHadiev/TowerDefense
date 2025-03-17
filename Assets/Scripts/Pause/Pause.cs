using UnityEngine;

public class Pause
{
    private readonly IAdvertising _advertising;
    private readonly GameplayMarkup _markup;

    public Pause(IAdvertising advertising, GameplayMarkup markup)
    {
        _advertising = advertising;
        _markup = markup;
    }

    public void Start()
    {
        _advertising.StickyBannerToggle(true);
        _markup.Start();
        Time.timeScale = 1;
    }

    public void Stop()
    {
        _advertising.ShowInterstitialAdv();
        _advertising.StickyBannerToggle(false);
        _markup.Stop();
        Time.timeScale = 0;
    }
}