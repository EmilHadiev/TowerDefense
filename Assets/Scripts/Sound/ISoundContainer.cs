public interface ISoundContainer
{
    void Play(BulletType bulletType);
    void Play(string soundType);
    void Stop();
}