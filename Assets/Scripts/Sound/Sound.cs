using System;
using UnityEngine;

[Serializable]
public struct Sound
{
    [field: SerializeField] public SoundType SoundType { get; private set; }
    [field: SerializeField] public AudioClip Clip { get; private set; }
}
