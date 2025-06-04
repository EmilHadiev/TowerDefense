public interface IGun
{
    public float DamagePercent { get; }
    public float BaseAttackSpeed { get; }

    public void SetData(GunData gunData);
}