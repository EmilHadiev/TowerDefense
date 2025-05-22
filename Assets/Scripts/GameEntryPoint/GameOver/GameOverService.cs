using System;
using Zenject;

public class GameOverService : IGameOver, IInitializable, IDisposable
{
    private readonly IHealth _playerHealth;

    public event Action GameOvered;

    public GameOverService(IPlayer player)
    {
        _playerHealth = player.Health;
    }

    public void Initialize() => _playerHealth.Died += GameOver;

    public void Dispose() => _playerHealth.Died -= GameOver;

    public void GameOver()
    {
        GameOvered?.Invoke();
    }
}