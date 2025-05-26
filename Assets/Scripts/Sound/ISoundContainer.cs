public interface ISoundContainer
{
    /// <summary>
    /// you need to get the name from SoundName it's a static class
    /// </summary>
    /// <param name="soundName"></param>
    void Play(string soundName);
    void Stop();
}