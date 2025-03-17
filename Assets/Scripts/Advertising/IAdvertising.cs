using System;

public interface IAdvertising
{
    void StickyBannerToggle(bool isOn);
    void ShowInterstitialAdv();
    void ShowRewardAdv(AdvType advType, string rewardValue = "", Action callBack = null);
}
