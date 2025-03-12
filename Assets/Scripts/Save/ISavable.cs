namespace YG
{
    public partial class SavesYG
    {
        public int coins;
    }
}

public interface ISavable
{
    public int Coins { get; set; }

    void LoadProgress();

    void SaveProgress();

    //carefully resets all saves without the ability to restore
    void ResetAllSavesAndProgress();
}