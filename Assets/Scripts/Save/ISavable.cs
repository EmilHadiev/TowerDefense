public interface ISavable
{
    void LoadProgress();

    void SaveProgress();

    //carefully resets all saves without the ability to restore
    void ResetAllSavesAndProgress();
}