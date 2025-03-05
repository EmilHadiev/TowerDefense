public abstract class Upgrader
{
    protected readonly PlayerStat Stat;

    protected abstract UpgradeType UpgradeType { get; }

    public readonly UpgradeData Data;

    public Upgrader(PlayerStat stat, UpgradeData data)
    {
        Stat = stat;
        Data = data;
    }

    public abstract void Upgrade();
}