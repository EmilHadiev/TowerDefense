public class YandexPause : Pause
{
    private readonly GameplayMarkup _markup;

    public YandexPause(IAdvertising advertising, GameplayMarkup markup) : base(advertising)
    {
        _markup = markup;
    }

    public override void Start()
    {
        base.Start();
        Advertising.StickyBannerToggle(false);
        _markup.Start();
    }

    public override void Stop()
    {
        base.Stop();
        Advertising.StickyBannerToggle(true);
        _markup.Stop();
    }
}