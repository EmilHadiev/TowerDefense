using UnityEngine;

[CreateAssetMenu(menuName = "Data/LevelTracker", fileName = "LevelTracker")]
public class LevelTracker : ScriptableObject
{
    /// <summary>
    /// use set only to load data. If you want to increase the value use the method TryAddCompletedLevel().
    /// </summary>
    [field: SerializeField] public int NumberLevelsCompleted { get; set; }
    [field: SerializeField] public int CurrentLevel { get; set; }

    public bool IsNotCompletedLevel => NumberLevelsCompleted == CurrentLevel;

    /// <summary>
    /// increase the value of completed levels
    /// </summary>

    public void TryAddCompletedLevel()
    {
        if (IsNotCompletedLevel)
        {
            NumberLevelsCompleted += 1;
            CurrentLevel = NumberLevelsCompleted;
        }
    }
}