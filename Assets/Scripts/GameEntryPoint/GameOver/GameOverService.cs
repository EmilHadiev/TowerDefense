using System;
using YG;
using Zenject;

public class GameOverService : IGameOver, IInitializable, IDisposable
{
    private readonly IHealth _playerHealth;

    public event Action PlayerLost;
    public event Action PlayerWon;

    public GameOverService(IPlayer player)
    {
        _playerHealth = player.Health;
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
    }
}