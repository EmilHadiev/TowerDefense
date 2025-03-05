using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStat", menuName = "Character/Stat/PlayerStats")]
public class PlayerStat : ScriptableObject
{
    [SerializeField, Range(1, 100f)] public float Health = 100;
    [SerializeField, Range(0.001f, 10f)] public float AttackSpeed = 1f;
    [SerializeField, Range(0.001f, 10f)] public float Damage = 10;
}