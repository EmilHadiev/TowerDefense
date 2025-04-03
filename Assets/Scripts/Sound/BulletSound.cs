using UnityEngine;

public struct BulletSound
{
    public readonly BulletType BulletType;
    public readonly AudioClip Clip;

    public BulletSound(BulletType bulletType, AudioClip clip)
    {
        BulletType = bulletType;
        Clip = clip;
    }
}