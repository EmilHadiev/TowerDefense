using System.Collections.Generic;
using System.Linq;

public abstract class Upgrader
{
    public const int UpgradePrice = 50;

    protected readonly GunData[] GunData;

    public Upgrader(IEnumerable<GunData> gunData)
    {
        GunData = gunData.ToArray();
    }

    public abstract void Upgrade();

    public abstract string GetUpgradeDescription();
}