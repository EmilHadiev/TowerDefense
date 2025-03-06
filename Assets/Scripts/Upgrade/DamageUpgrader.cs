public class DamageUpgrader : Upgrader
{
    protected override UpgradeType UpgradeType { get; }

    public DamageUpgrader(PlayerStat stat, UpgradeData data) : base(stat, data)
    {
        UpgradeType = UpgradeType.Damage;
    }

    public override void Upgrade()
    {
        
    }
}
