using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStat", menuName = "Character/Stat/PlayerStats")]
public class PlayerStat : ScriptableObject
{
    [field: SerializeField, Range(1, 100f)] private float _health = 100;
    [field: SerializeField, Range(0.001f, 10f)] private float _attackSpeed = 0.2f;
    [field: SerializeField, Range(0.001f, 10f)] private float _damage = 10;

    public Property Health { get; private set; }
    public Property AttackSpeed { get; private set; }
    public Property Damage { get; private set; }

    private void Awake()
    {
        Health = new Property(_health);
        AttackSpeed = new Property(_attackSpeed);
        Damage = new Property(_damage);
    }
}