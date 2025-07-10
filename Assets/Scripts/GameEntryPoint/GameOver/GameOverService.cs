using System;
using YG;
using Zenject;

public class GameOverService : IGameOver, IInitializable, IDisposable
{
    private readonly IHealth _playerHealth;
    private readonly LevelTracker _levelTracker;
    private readonly PlayerStat _stat;

    public event Action PlayerLost;
    public event Action PlayerWon;

    public GameOverService(IPlayer player, LevelTracker levelTracker, PlayerStat playerStat)
    {
        _playerHealth = player.Health;
        _levelTracker = levelTracker;
        _stat = playerStat;
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
        _levelTracker.NumberLevelsCompleted += 1;
        _stat.MaxHealth += Constants.AdditionalHealthToPlayer;
        YG2.onCloseAnyAdv();
        PlayerWon?.Invoke();
    }
}