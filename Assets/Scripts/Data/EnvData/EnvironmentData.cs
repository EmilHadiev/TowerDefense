using UnityEngine;

[CreateAssetMenu(menuName = "GameEnvData/Game", fileName = "GameEnvData")]
public class EnvironmentData : ScriptableObject
{
    [field: SerializeField] public bool IsDesktop { get; set; }
    [field: SerializeField] public LanguageType Language { get; set; }
    [field: SerializeField] public PlayerData PlayerData { get; set; }
}