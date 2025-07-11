using UnityEngine;

/// <summary>
/// this includes the level of enemies and the number of levels the player has completed
/// </summary>
[CreateAssetMenu(menuName = "Data/LevelTracker", fileName = "LevelTracker")]
public class LevelTracker : ScriptableObject
{
    [field: SerializeField] public int NumberLevelsCompleted { get; set; }
}