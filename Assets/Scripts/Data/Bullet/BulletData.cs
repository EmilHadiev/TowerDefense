using UnityEngine;

[CreateAssetMenu(fileName = "BulletData", menuName = "Bullet")]
public class BulletData : ScriptableObject, IBulletData, IBulletDescription
{
    [field: SerializeField, Range(1, 100f)] public float Speed { get; private set; }
    [field: SerializeField, Range(0, 100f)] public float Damage { get; private set; }
    [field: SerializeField, Range(1, 5)] public int LifeTime { get; private set; }
    [field: SerializeField] public Color Color { get; private set; }
    [field: SerializeField] public AudioClip Clip { get; private set; }

    [field: SerializeField] public Sprite Sprite { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public string Description { get; private set; }
    [field: SerializeField, TextArea(1, 3)] public string FullDescription { get; private set; }
    [field: SerializeField] public int Price { get; private set; }
    [field: SerializeField] public bool IsPurchased { get; set; }
}