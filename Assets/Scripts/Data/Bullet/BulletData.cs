using UnityEngine;

[CreateAssetMenu(fileName = "BulletData", menuName = "Bullet")]
public class BulletData : ScriptableObject
{
    [SerializeField, Range(1, 100f)] public float Speed;
    [SerializeField, Range(0, 100f)] public float Damage;
    [SerializeField, Range(1, 5)] public int LifeTime;

    [field: SerializeField] public Color Color { get; private set; }
    [field: SerializeField] public Sprite Sprite { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public string Description { get; private set; }
    [field: SerializeField, TextArea(1, 3)] public string FullDescription { get; private set; }
}