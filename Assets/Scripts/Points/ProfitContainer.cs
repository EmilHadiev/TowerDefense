public class ProfitContainer : IProfitContainer
{
    private int _currentProfit;

    public int Profit => _currentProfit;

    public void GetBoostProfits(int multiplier)
    {
        _currentProfit *= multiplier;
    }

    public void IncreaseProfits(int profit)
    {
        _currentProfit += profit;
    }
}