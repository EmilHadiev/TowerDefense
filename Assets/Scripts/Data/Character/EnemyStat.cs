using UnityEngine;

public abstract class EnemyStat : ScriptableObject
{
    [SerializeField, Range(1f, 100f)] public float Health = 30;
    [SerializeField, Range(1f, 100f)] public float Damage = 10;
    [SerializeField, Range(1f, 20f)] public float Speed = 5;
    [SerializeField, Range(1, 10f)] public float AttackRadius = 1.5f;

    [field: SerializeField] public EnemyType EnemyType { get; protected set; }
}