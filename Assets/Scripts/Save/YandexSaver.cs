using System;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class YandexSaver : ISavable, IDisposable
{
    private readonly ICoinStorage _coinStorage;
    private readonly IEnumerable<UpgradeData> _data;
    private readonly PlayerStat _playerStat;
    private readonly EnemyLevelData _levelData;

    private UpgradeItemsSaver _upgradeSaver;

    private int Coins
    {
        get => YG2.saves.coins;

        set => YG2.saves.coins = value;
    }

    public YandexSaver(ICoinStorage coinStorage, IEnumerable<UpgradeData> data, PlayerStat playerStat, EnemyLevelData levelData)
    {
        _coinStorage = coinStorage;
        _data = data;
        _playerStat = playerStat;
        _levelData = levelData;
    }

    public void Dispose() => SaveProgress();

    public void LoadProgress()
    {
        LoadCoins();
        LoadUpgraders();
        LoadPlayerStat();
        LoadEnemyLevel();

        Debug.Log("Progress loaded...");
    }

    public void ResetAllSavesAndProgress() => YG2.SetDefaultSaves();

    public void SaveProgress()
    {
        SaveData();
        YG2.SaveProgress();
    }

    private void SaveData()
    {
        SaveCoins();
        SaveUpgraders();
        SavePlayerStat();
        SaveEnemyLevel();
    }

    #region Coins
    private void LoadCoins()
    {
        if (Coins != _coinStorage.Coins)
            _coinStorage.Add(Coins);
    }
    private void SaveCoins() => Coins = _coinStorage.Coins;
    #endregion

    #region Upgraders
    private void LoadUpgraders() => InitItems();

    private void InitItems() => _upgradeSaver = new UpgradeItemsSaver(YG2.saves.UpgradeItems, _data);

    private void SaveUpgraders()
    {
        Debug.Log(_upgradeSaver == null);
        _upgradeSaver.SaveUpgraders();
    }

    #endregion

    #region PlayerStat
    private void LoadPlayerStat()
    {
        _playerStat.MaxHealth = YG2.saves.playerHealth;
        _playerStat.Damage = YG2.saves.playerDamage;
        _playerStat.AttackSpeed = YG2.saves.playerAttackSpeed;
    }

    private void SavePlayerStat()
    {
        YG2.saves.playerHealth = _playerStat.MaxHealth;
        YG2.saves.playerDamage = _playerStat.Damage;
        YG2.saves.playerAttackSpeed = _playerStat.AttackSpeed;
    }
    #endregion

    #region EnemyLevel

    private void LoadEnemyLevel() => _levelData.Level = YG2.saves.enemyLevel;

    private void SaveEnemyLevel() => YG2.saves.enemyLevel = _levelData.Level;
    #endregion
}