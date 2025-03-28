using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using static YG.SavesYG;
namespace YG
{
    [Serializable]
    public class UpgradeItem
    {
        public int Cost;
        public UpgradeType Type;
    }

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
        public float playerAttackSpeed = 0.25f;
        public float playerDamage = 0;
        #endregion

        #region EnemyLevelData
        public int enemyLevel = 0;
        #endregion
    }
}

public sealed class SAVES_YG_PLACE
{

}