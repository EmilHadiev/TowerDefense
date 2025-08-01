using System;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class YandexSaver : ISavable, IDisposable
{
    private readonly ICoinStorage _coinStorage;
    private readonly PlayerStat _playerStat;
    private readonly LevelTracker _levelData;
    private readonly IBulletDefinition[] _bullets;
    private readonly GunData[] _guns;
    private readonly PlayerData[] _playerData;
    private readonly TrainingData _trainingData;

    private BulletItemSaver _bulletSaver;
    private GunItemSaver _gunSaver;
    private PlayerDataSaver _playerDataSaver;
    

    private int Coins
    {
        get => YG2.saves.coins;

        set => YG2.saves.coins = value;
    }

    public YandexSaver(ICoinStorage coinStorage, PlayerStat playerStat, LevelTracker levelData, IBulletDefinition[] bullets, GunData[] guns, PlayerData[] playerData, TrainingData trainingData)
    {
        _coinStorage = coinStorage;
        _playerStat = playerStat;
        _levelData = levelData;
        _bullets = bullets;
        _guns = guns;
        _playerData = playerData;
        _trainingData = trainingData;
    }

    public void Dispose() => SaveProgress();

    public void LoadProgress()
    {
        LoadCoins();
        LoadPlayerStat();
        LoadCompletedLevel();        
        InitBullets();
        InitGuns();
        InitPlayerData();
        SavePlayerData();
        LoadTraining();

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
        SavePlayerStat();
        SaveCompletedLevel();
        SaveBullets();
        SaveGuns();
        SavePlayerData();
        SaveTraining();
    }

    #region Coins
    private void LoadCoins()
    {
        if (Coins != _coinStorage.Coins)
            _coinStorage.Add(Coins);
    }
    private void SaveCoins() => Coins = _coinStorage.Coins;
    #endregion

    #region PlayerStat
    private void LoadPlayerStat()
    {
        _playerStat.MaxHealth = YG2.saves.playerHealth;
    }

    private void SavePlayerStat()
    {
        YG2.saves.playerHealth = _playerStat.MaxHealth;
    }
    #endregion

    #region EnemyLevel

    private void LoadCompletedLevel()
    {
        _levelData.NumberLevelsCompleted = YG2.saves.compltetedLevels;
        _levelData.CurrentLevel = YG2.saves.compltetedLevels;
    }

    private void SaveCompletedLevel() => YG2.saves.compltetedLevels = _levelData.NumberLevelsCompleted;
    #endregion

    #region Bullets
    private void InitBullets() => _bulletSaver = new BulletItemSaver(_bullets, YG2.saves.BulletItems);

    private void SaveBullets()
    {
        if (_bulletSaver == null)
            Debug.Log($"{nameof(_bulletSaver)} is null");
        else
            _bulletSaver?.Save();
    }
    #endregion

    #region Guns
    private void InitGuns() => _gunSaver = new GunItemSaver(_guns, YG2.saves.GunItems);

    private void SaveGuns()
    {
        if (_gunSaver == null)
            Debug.Log($"{nameof(_gunSaver)} is null");
        else
            _gunSaver?.Save();
    }

    #endregion

    #region PlayerData
    private void InitPlayerData() => _playerDataSaver = new PlayerDataSaver(_playerData, YG2.saves.PlayerDataItems);

    private void SavePlayerData()
    {
        if (_playerDataSaver == null)
            Debug.Log($"{nameof(_playerDataSaver)} is null");
        else
            _playerDataSaver?.Save();
    }
    #endregion

    #region Training
    private void LoadTraining() => _trainingData.IsTrainingCompleted = YG2.saves.IsTrainingCompleted;

    private void SaveTraining() => YG2.saves.IsTrainingCompleted = _trainingData.IsTrainingCompleted;
    #endregion
}