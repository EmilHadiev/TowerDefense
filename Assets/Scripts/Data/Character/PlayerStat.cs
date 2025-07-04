using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStat", menuName = "Character/Stat/PlayerStats")]
public class PlayerStat : ScriptableObject
{
    [field: SerializeField, Range(1, 100f)] public float Health = 100;
    [field: SerializeField, Range(1, 100f)] public float MaxHealth = 100;
    [field: SerializeField, Range(0.001f, 1000f)] public int BonusAttackSpeed { get; set; }
    [field: Obsolete, SerializeField, Range(1, 5f)] public float Speed = 3;

    public Property HealthProperty { get; private set; }

    private void Awake()
    {
        HealthProperty = new Property(MaxHealth);
    }
}