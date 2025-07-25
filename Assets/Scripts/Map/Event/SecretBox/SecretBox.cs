public abstract class SecretBox
{
    private readonly ISoundContainer _soundContainer;

    public SecretBox(ISoundContainer soundContainer)
    {
        _soundContainer = soundContainer;
    }

    public abstract void Activate();

    protected void PlaySound(string soundName)
    {
        _soundContainer.Play(soundName);
    }
}