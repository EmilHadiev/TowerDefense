using UnityEngine;

[CreateAssetMenu(fileName = "BulletData", menuName = "Bullet")]
public class BulletData : ScriptableObject
{
    [SerializeField, Range(1, 100f)] public float Speed;
    [SerializeField, Range(1, 100f)] public float Damage;
    [SerializeField, Range(1, 5)] public int LifeTime;

    [field: SerializeField] public Color Color { get; private set; }
}