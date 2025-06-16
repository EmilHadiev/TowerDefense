using System;

public interface IGameOver
{
    event Action PlayerLost;
    event Action PlayerWon;

    void GameOver();
    void GameCompleted();
}
