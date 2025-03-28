using UnityEngine;

[CreateAssetMenu(menuName = "Data/EnemyLevel", fileName = "EnemyLevel")]
class EnemyLevelData : ScriptableObject
{
    [field: SerializeField] public int Level { get; set; }
}