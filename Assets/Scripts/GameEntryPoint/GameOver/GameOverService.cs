using System;
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
        YG2.onCloseAnyAdv();
        PlayerLost?.Invoke();
    }

    public void GameCompleted()
    {
        AddLevel();
        RewardPlayer();
        YG2.onCloseAnyAdv();
        PlayerWon?.Invoke();
    }

    private void AddLevel()
    {
        _levelTracker.TryAddCompletedLevel();
    }

    private void RewardPlayer()
    {
        _upgrader.Upgrade();
        _awardGiver.GiveAward();
    }
}