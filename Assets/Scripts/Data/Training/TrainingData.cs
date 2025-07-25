using UnityEngine;

[CreateAssetMenu(menuName = "TrainingMode", fileName = "TrainingMode/Data")]
public class TrainingData : ScriptableObject
{
    [field: SerializeField] public PlayerTrainingView ViewPrefab { get; private set; }
    [field: SerializeField] public bool IsTrainingCompleted { get; set; }
}