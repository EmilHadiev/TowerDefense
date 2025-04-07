using UnityEngine;

public interface IBulletDescription
{
    string Description { get; }
    string FullDescription { get; }
    string Name { get; }
    Sprite Sprite { get; }
    int Price { get; }
    bool IsPurchased { get; set; }
}