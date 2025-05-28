using System;

public interface IAdvertising
{
    void StickyBannerToggle(bool isOn);
    void ShowInterstitialAdv();
    void ShowRewardAdv(Action callBack);
}
