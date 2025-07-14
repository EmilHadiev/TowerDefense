public class AwardGiver
{
    private ILootable _loot;

    public void SetReward(ILootable loot = null)
    {
        _loot = loot;
    }

    public void GiveAward()
    {
        if (_loot != null)
            _loot.IsDropped = true;
    }
}