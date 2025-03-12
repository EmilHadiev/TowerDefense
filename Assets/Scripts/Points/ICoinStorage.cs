using System;

public interface ICoinStorage
{
    event Action<int> CoinsChanged;

    public int Coins { get; }

    void Add(int coin);
    bool TrySpend(int coin);
}