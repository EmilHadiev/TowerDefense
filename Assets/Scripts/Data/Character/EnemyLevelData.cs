using UnityEngine;

[CreateAssetMenu(menuName = "Data/EnemyLevel", fileName = "EnemyLevel")]
public class EnemyLevelData : ScriptableObject
{
    [field: SerializeField] public int Level { get; set; }
}