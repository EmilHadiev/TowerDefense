using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using static YG.SavesYG;
namespace YG
{
    public partial class SavesYG
    {
        #region Coins
        public int coins = 0;
        #endregion

        #region UpgradeData
        //data
        [Serializable]
        public class UpgradeItem
        {
            public int Cost;
            public UpgradeType Type;
        }

        public List<UpgradeItem> UpgradeItems = new List<UpgradeItem>(3);

        /*public List<UpgradeItem> InitAndGetUpgradeItems()
        {
            if (UpgradeItems.Count == 0 || UpgradeItems == null)
            {
                Debug.Log("Устанваливаю новые значения!");
                UpgradeItems = new List<UpgradeItem>(3)
                {
                    new UpgradeItem() { Cost = Constants.UpgradeStartPrice, Type = UpgradeType.Health },
                    new UpgradeItem() { Cost = Constants.UpgradeStartPrice, Type = UpgradeType.AttackSpeed },
                    new UpgradeItem() { Cost = Constants.UpgradeStartPrice, Type = UpgradeType.Damage }
                };               
            }

            return UpgradeItems;
        }*/

        public void ShowSize()
        {            
            Debug.Log(UpgradeItems.Count);
        }

        private void SetCost(UpgradeData upgradeData, List<UpgradeItem> upgradeItems)
        {
            upgradeData.Cost = upgradeItems.FirstOrDefault(data => data.Type == upgradeData.UpgradeType).Cost;
        }
        #endregion
    }
}

public interface SAVES_YG_PLACE
{

}