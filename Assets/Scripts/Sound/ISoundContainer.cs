public interface ISoundContainer
{
    void Play(BulletType bulletType);
    void Play(SoundType soundType);
    void Stop();
}