public interface ISavable
{
    void LoadProgress();

    void SaveProgress();

    /// <summary>
    /// carefully resets all saves without the ability to restore
    /// </summary>
    void ResetAllSavesAndProgress();
}