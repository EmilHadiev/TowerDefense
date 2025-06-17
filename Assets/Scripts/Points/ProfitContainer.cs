using Zenject;

public class ProfitContainer : IProfitContainer, IInitializable
{
    public int Profits { get; private set; }

    public int GetBoostProfits(int multiplier = 2) => Profits * multiplier;

    public void IncreaseProfits(int profits) => Profits += profits;

    public void Initialize()
    {
        Profits = 0;
    }
}