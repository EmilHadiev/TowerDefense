using System;
using YG;
using Zenject;

public class GameOverService : IGameOver, IInitializable, IDisposable
{
    private readonly IHealth _playerHealth;
    private readonly LevelTracker _levelTracker;

    public event Action PlayerLost;
    public event Action PlayerWon;

    public GameOverService(IPlayer player, LevelTracker levelTracker)
    {
        _playerHealth = player.Health;
        _levelTracker = levelTracker;
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
        _levelTracker.NumberLevelsCompleted += 1;
        YG2.onCloseAnyAdv();
    }
}