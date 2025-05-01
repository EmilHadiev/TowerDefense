using UnityEngine;

public class BulletEffectView
{
    private readonly ISoundContainer _soundContainer;

    public BulletEffectView(ISoundContainer soundContainer)
    {
        _soundContainer = soundContainer;
    }

    public void ChangeTargetParticleColor(Collider collider, Color color)
    {
        if (collider.TryGetComponent(out IParticleColorChangable viewContainer))
            viewContainer.SetDamageParticleColor(color);
    }

    public void PlaySound(BulletType type) => _soundContainer.Play(type);
}
