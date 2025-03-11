using UnityEngine;

public class UpgradeAdvContainer : AdvertisingContainer
{
    [SerializeField] private UpgradeData[] _data;

    private const AdvType Type = AdvType.Coin;

    protected override void OnClick()
    {
        Advertising.ShowRewardAdv(Type, GetRewardValue());
    }

    public void AddPrice(int cost)
    {

    }

    private string GetRewardValue()
    {
        return "100";
    }
}