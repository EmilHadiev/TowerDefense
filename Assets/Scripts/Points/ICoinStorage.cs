using System;

public interface ICoinStorage
{
    event Action<int> CoinsChanged;

    void Add(int coin);
    bool TrySpend(int coin);
}
