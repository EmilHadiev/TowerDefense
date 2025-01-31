using System;

public class CoinStorage : ICoinStorage
{
    private int _coins;

    public event Action<int> CoinsChanged;

    public void Add(int coins)
    {
        IsValidValue(coins);

        _coins += coins;
        CoinsChanged?.Invoke(_coins);
    }

    public bool TrySpend(int coins)
    {
        IsValidValue(_coins);

        if (_coins - coins < 0)
            return false;

        _coins -= coins;
        CoinsChanged?.Invoke(_coins);

        return true;
    }

    private bool IsValidValue(int coins)
    {
        if (coins < 0)
            throw new ArgumentOutOfRangeException(nameof(coins));

        return true;
    }
}