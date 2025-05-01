using System.Collections.Generic;

public class BulletPool : IPool<Bullet>
{
    private const int StartSize = 300;

    private readonly List<Bullet> _bullets = new List<Bullet>(StartSize);

    public void Add(Bullet bullet) => _bullets.Add(bullet);

    public bool TryGet(out Bullet bullet)
    {
        foreach (var item in _bullets)
        {
            if (item.gameObject.activeInHierarchy == false)
            {
                bullet = item;
                return true;
            }
        }

        bullet = null;
        return false;
    }
}