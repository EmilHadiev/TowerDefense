using UnityEngine;
using Zenject;

public class BulletEffectView : MonoBehaviour
{
    [SerializeField] private Bullet _bullet;

    private ISoundContainer _soundContainer;

    private void OnValidate()
    {
        _bullet ??= GetComponent<Bullet>();
    }

    private void OnEnable() => PlaySoundEffect();

    [Inject]
    private void Constructor(ISoundContainer soundContainer)
    {
        _soundContainer = soundContainer;
    }

    public void ChangeTargetParticleColor(Collider collider, Color color)
    {
        if (collider.TryGetComponent(out IParticleColorChangable viewContainer))
            viewContainer.SetDamageParticleColor(color);
    }

    private void PlaySoundEffect()
    {
        _soundContainer.Stop();
        _soundContainer.Play(_bullet.Type);
    }
}