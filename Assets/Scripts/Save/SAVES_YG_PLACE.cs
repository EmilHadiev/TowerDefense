using System;
using System.Collections.Generic;

[Serializable]
public class BulletItem
{
    public BulletType Type;
    public bool IsDropped;
}

[Serializable]
public class GunItem
{
    public int Id;
    public bool IsDropped;
    public float BaseDamage;
    public float AttackSpeedPercent;
}

[Serializable]
public class PlayerDataItem
{
    public int Id;
    public bool IsPurchased;
}

namespace YG
{
    public partial class SavesYG
    {
        #region Coins
        public int coins = 0;
        #endregion

        #region PlayerStat
        public float playerHealth = 100;
        #endregion

        #region EnemyLevelData
        public int enemyLevel = 0;
        #endregion

        #region Bullets
        public List<BulletItem> BulletItems = new List<BulletItem>(10);
        #endregion

        #region Gundata
        public List<GunItem> GunItems = new List<GunItem>(10);
        #endregion

        #region PlayerData
        public List<PlayerDataItem> PlayerDataItems = new List<PlayerDataItem>(9);
        #endregion

        #region Training
        public bool IsTrainingCompleted = false;
        #endregion
    }
}

public sealed class SAVES_YG_PLACE
{

}