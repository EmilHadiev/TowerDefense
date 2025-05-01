using System;
using Zenject;

public class BulletCreator
{
    private readonly BulletPoolContainer _pools;
    private readonly Bullet[] _bullets;
    private readonly BulletEffectSetter _effectSetter;
    private readonly Action<int> _setEffect;
    private readonly IInstantiator _instantiator;

    public Bullet this[int index]
    {
        get => _bullets[index];
    }

    public BulletCreator(IInstantiator instantiator, Bullet[] bullets, BulletPoolContainer pools, BulletEffectSetter effectSetter, Action<int> setEffect)
    {
        _pools = pools;
        _bullets = bullets;
        _effectSetter = effectSetter;
        _setEffect = setEffect;
        _instantiator = instantiator;
    }

    public void FirstInit(int poolSize = 30)
    {
        for (int i = 0; i < _bullets.Length; i++)
            CreateBullets(poolSize, i);
    }

    public void CreateBullets(int poolSize, int index)
    {
        for (int i = 0; i < poolSize; i++)
            CreateTemplate(GetBulletByIndex(index), _pools[index]);
    }

    public bool TryGetSelectBullet(int selectIndex, out Bullet bullet)
    {
        if (_pools[selectIndex].TryGet(out bullet))
            return true;

        return false;            
    }

    private void CreateTemplate(Bullet template, IPool<Bullet> pool)
    {
        Bullet bullet = _instantiator.InstantiatePrefab(template).GetComponent<Bullet>();
        bullet.InitEffects(_setEffect);
        bullet.gameObject.SetActive(false);
        bullet.transform.parent = null;
        pool.Add(bullet);
        _effectSetter.AddBullet(bullet);
    }

    private Bullet GetBulletByIndex(int index) => _bullets[index];
}