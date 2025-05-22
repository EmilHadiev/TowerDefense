using System;

public interface IGameOver
{
    event Action GameOvered;
    void GameOver();
}
