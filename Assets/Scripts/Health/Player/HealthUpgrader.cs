public class HealthUpgrader
{
    private readonly PlayerStat _stat;

    public HealthUpgrader(PlayerStat stat)
    {
        _stat = stat;
    }

    public void Upgrade() => _stat.MaxHealth += Constants.AdditionalHealthToPlayer;
}