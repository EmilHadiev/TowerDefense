using System;
using System.Collections.Generic;
using System.Linq;
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

    /// <summary>
    /// Dropped bullets. Where IsDropped (ILootable) is true
    /// </summary>
    public IEnumerable<Bullet> AvailableBullets => _bullets;

    public BulletCreator(IInstantiator instantiator, Bullet[] bullets, BulletPoolContainer pools, BulletEffectSetter effectSetter, Action<int> setEffect)
    {
        _pools = pools;
        _bullets = GetDroppedBullets(bullets);
        _effectSetter = effectSetter;
        _setEffect = setEffect;
        _instantiator = instantiator;
    }

    public void FirstInit(int poolSize = 30)
    {
        for (int i = 0; i < _bullets.Length; i++)
        {
            if (_bullets[i].BulletDescription.IsDropped == false)
                continue;

            CreateBullets(poolSize, i);
        }
    }

    public void CreateBullets(int poolSize, int index)
    {
        for (int i = 0; i < poolSize; i++)
        {
            Bullet template = GetBulletByIndex(index);

            if (template.BulletDescription.IsDropped == false)
                continue;

            CreateTemplate(template, _pools[index]);
        }
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

    private Bullet[] GetDroppedBullets(Bullet[] bullets) =>
        bullets.Where(bullet => bullet.BulletDescription.IsDropped == true).ToArray();
}