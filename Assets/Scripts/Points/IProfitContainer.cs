public interface IProfitContainer
{
    public int Profit { get; }

    public void IncreaseProfits(int profit);
    public void GetBoostProfits(int multiplier);
}
