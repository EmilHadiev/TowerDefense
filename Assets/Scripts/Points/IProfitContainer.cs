public interface IProfitContainer
{
    public int Profits { get; }

    public void IncreaseProfits(int profits);
    public int GetBoostProfits(int multiplier = 2);
    public void AddProfitsToPlayer();
}
