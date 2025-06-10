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
    private readonly IBulletDefinition[] _bullets;
    private readonly GunData[] _guns;
    private readonly PlayerData[] _playerData;

    private UpgradeItemsSaver _upgradeSaver;
    private BulletItemSaver _bulletSaver;
    private GunItemSaver _gunSaver;
    private PlayerDataSaver _playerDataSaver;
    

    private int Coins
    {
        get => YG2.saves.coins;

        set => YG2.saves.coins = value;
    }

    public YandexSaver(ICoinStorage coinStorage, IEnumerable<UpgradeData> data, PlayerStat playerStat, EnemyLevelData levelData, IBulletDefinition[] bullets, GunData[] guns, PlayerData[] playerData)
    {
        _coinStorage = coinStorage;
        _data = data;
        _playerStat = playerStat;
        _levelData = levelData;
        _bullets = bullets;
        _guns = guns;
        _playerData = playerData;
    }

    public void Dispose() => SaveProgress();

    public void LoadProgress()
    {
        LoadCoins();
        LoadUpgraders();
        LoadPlayerStat();
        LoadEnemyLevel();
        InitBullets();
        InitGuns();
        InitPlayerData();
        SavePlayerData();

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
        SaveBullets();
        SaveGuns();
        SavePlayerData();
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
    private void LoadUpgraders() => InitUpgraders();

    private void InitUpgraders() => _upgradeSaver = new UpgradeItemsSaver(YG2.saves.UpgradeItems, _data);

    private void SaveUpgraders()
    {
        Debug.Log(_upgradeSaver == null);
        _upgradeSaver.Save();
    }

    #endregion

    #region PlayerStat
    private void LoadPlayerStat()
    {
        _playerStat.MaxHealth = YG2.saves.playerHealth;
        _playerStat.Damage = YG2.saves.playerDamage;
        _playerStat.BonusAttackSpeed = YG2.saves.playerBonusAttackSpeed;
    }

    private void SavePlayerStat()
    {
        YG2.saves.playerHealth = _playerStat.MaxHealth;
        YG2.saves.playerDamage = _playerStat.Damage;
        YG2.saves.playerBonusAttackSpeed = _playerStat.BonusAttackSpeed;
    }
    #endregion

    #region EnemyLevel

    private void LoadEnemyLevel() => _levelData.Level = YG2.saves.enemyLevel;

    private void SaveEnemyLevel() => YG2.saves.enemyLevel = _levelData.Level;
    #endregion

    #region Bullets
    private void InitBullets() => _bulletSaver = new BulletItemSaver(_bullets, YG2.saves.BulletItems);

    private void SaveBullets() => _bulletSaver.Save();
    #endregion

    #region Guns
    private void InitGuns() => _gunSaver = new GunItemSaver(_guns, YG2.saves.GunItems);

    private void SaveGuns() => _gunSaver.Save();

    #endregion

    #region PlayerData
    private void InitPlayerData() => _playerDataSaver = new PlayerDataSaver(_playerData, YG2.saves.PlayerDataItems);

    private void SavePlayerData() => _playerDataSaver.Save();
    #endregion
}