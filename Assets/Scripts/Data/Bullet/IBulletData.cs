using UnityEngine;

public interface IBulletData
{
    Color Color { get; }
    AudioClip Clip { get; }
    float Damage { get; }
    int LifeTime { get; }
    float Speed { get; }    
}