using System.Collections.Generic;

public class BulletEffectSetter
{
    private readonly ISoundContainer _sound;
    private readonly List<Bullet> _bullets;

    public BulletEffectSetter(ISoundContainer sound)
    {
        _sound = sound;
        _bullets = new List<Bullet>(15);
    }

    public void AddBullet(Bullet bullet)
    {
        _bullets.Add(bullet);
        SetBulletSound(bullet.Type);
        SetBulletEffect(bullet.Type);
    }

    public void SetBulletSound(BulletType bulletType) => _sound.SetBulletSound(bulletType);

    public void SetBulletEffect(BulletType type)
    {
        switch (type)
        {
            case BulletType.Fireball:
                SetEffectHandler<FireballBulletEffect>();
                break;
            case BulletType.Electric:
                SetEffectHandler<ElectricBulletEffect>();
                break;
            case BulletType.Ice:
                SetEffectHandler<IceBulletEffect>();
                break;
            case BulletType.Pushing:
                SetEffectHandler<BulletPushingEffect>();
                break;
            default:
                SetEffectHandler<EmptyEffect>();
                break;
        }
    }

    private void SetEffectHandler<T>() where T : IBulletEffectHandler
    {
        for (int i = 0; i < _bullets.Count; i++)
            _bullets[i].SetEffect<T>();
    }
}