using UnityEngine;

public interface IBulletData
{
    Color Color { get; }
    AudioClip Clip { get; }
    int LifeTime { get; }
    float Speed { get; }
}