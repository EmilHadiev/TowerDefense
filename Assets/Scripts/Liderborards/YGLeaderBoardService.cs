using YG;

public class YGLeaderBoardService : ILeaderBoardSevrice
{
    private const string TableName = "CompletedLevels";

    private readonly LevelTracker _levelTracker;

    public YGLeaderBoardService(LevelTracker levelTracker)
    {
        _levelTracker = levelTracker;
    }

    public void TrySaveValue()
    {
        if (_levelTracker.IsNotCompletedLevel)
            YG2.SetLeaderboard(TableName, _levelTracker.NumberLevelsCompleted);
    }
}