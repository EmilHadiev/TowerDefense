public interface IAdvertising
{
    void StickyBannerToggle(bool isOn);
    bool TryShowInterstitialAdv();
    void ShowRewardAdv(AdvType advType, string rewardValue = "");
}
