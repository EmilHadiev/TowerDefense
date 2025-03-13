using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStat", menuName = "Character/Stat/PlayerStats")]
public class PlayerStat : ScriptableObject
{
    [field: SerializeField, Range(1, 100f)] public float Health = 100;
    [field: SerializeField, Range(1, 100f)] public float MaxHealth = 100;
    [field: SerializeField, Range(0.001f, 10f)] public float AttackSpeed = 0.2f;
    [field: SerializeField, Range(0.001f, 10f)] public float Damage = 10;

    public Property HealthProperty { get; private set; }
    public Property DamageProperty { get; private set; }

    private void Awake()
    {
        HealthProperty = new Property(MaxHealth);
        DamageProperty = new Property(Damage);
    }
}