public class SecretBoxHealing : SecretBox
{
    private const int HealthPoints = 150;

    private readonly IHealth _health;

    public SecretBoxHealing(ISoundContainer soundContainer, IHealth health) : base(soundContainer)
    {
        _health = health;
    }

    public override void Activate()
    {
        _health.AddHealth(HealthPoints);
        PlaySound(SoundName.BloodHealing
            );
    }
}