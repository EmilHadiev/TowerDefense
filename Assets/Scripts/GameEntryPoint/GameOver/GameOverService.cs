﻿using System;
using UnityEngine;
using YG;
using Zenject;

public class GameOverService : IGameOver, IInitializable, IDisposable
{
    private readonly IHealth _playerHealth;
    private readonly LevelTracker _levelTracker;
    private readonly HealthUpgrader _upgrader;
    private readonly AwardGiver _awardGiver;

    public event Action PlayerLost;
    public event Action PlayerWon;

    public GameOverService(IPlayer player, LevelTracker levelTracker, HealthUpgrader upgrader, AwardGiver awardGiver)
    {
        _playerHealth = player.Health;
        _levelTracker = levelTracker;
        _upgrader = upgrader;
        _awardGiver = awardGiver;
    }

    public void Initialize() => _playerHealth.Died += GameOver;
    public void Dispose() => _playerHealth.Died -= GameOver;

    public void GameOver()
    {
        PlayerLost?.Invoke();
        YG2.onCloseAnyAdv();
    }

    public void GameCompleted()
    {
        PlayerWon?.Invoke();
        YG2.onCloseAnyAdv();
        RewardPlayer();
        AddLevel();
    }

    private void AddLevel()
    {
        _levelTracker.TryAddCompletedLevel();
    }

    private void RewardPlayer()
    {
        _upgrader.Upgrade();

        if (_awardGiver.IsRewardLevel())
            _awardGiver.GiveReward();
    }
}