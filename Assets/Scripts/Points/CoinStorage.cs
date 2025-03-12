using System;

public class CoinStorage : ICoinStorage
{
    public int Coins { get; private set; }

    public event Action<int> CoinsChanged;

    public void Add(int coins)
    {
        IsValidValue(coins);

        Coins += coins;
        CoinsChanged?.Invoke(Coins);
    }

    public bool TrySpend(int coins)
    {
        IsValidValue(Coins);

        if (Coins - coins < 0)
            return false;

        Coins -= coins;
        CoinsChanged?.Invoke(Coins);

        return true;
    }

    private bool IsValidValue(int coins)
    {
        if (coins < 0)
            throw new ArgumentOutOfRangeException(nameof(coins));

        return true;
    }
}