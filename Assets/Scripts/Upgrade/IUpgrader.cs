public interface IUpgrader
{
    public UpgradeData Data { get; }
    string GetUpgradeDescription();
    void Upgrade();
}