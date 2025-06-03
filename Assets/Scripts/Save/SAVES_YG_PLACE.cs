using System;
using System.Collections.Generic;

[Serializable]
public class UpgradeItem
{
    public int Cost;
    public UpgradeType Type;
}

[Serializable]
public class BulletItem
{
    public BulletType Type;
    public bool IsPurchased;
}

namespace YG
{
    public partial class SavesYG
    {
        #region Coins
        public int coins = 0;
        #endregion

        #region UpgradeData
        public List<UpgradeItem> UpgradeItems = new List<UpgradeItem>(3);
        #endregion

        #region PlayerData
        public float playerHealth = 100;
        public float playerBonusAttackSpeed = 0.25f;
        public float playerDamage = 0;
        #endregion

        #region EnemyLevelData
        public int enemyLevel = 0;
        #endregion

        #region Bullets
        public List<BulletItem> BulletItems = new List<BulletItem>(10);
        #endregion
    }
}

public sealed class SAVES_YG_PLACE
{

}