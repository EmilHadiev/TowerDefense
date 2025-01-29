using System;
using UnityEngine;

[Serializable]
public struct Sound
{
    [field: SerializeField] public BulletType BulletType { get; private set; }
    [field: SerializeField] public AudioClip Clip { get; private set; }
}
