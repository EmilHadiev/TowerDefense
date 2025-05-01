using System.Collections.Generic;

public class BulletPoolContainer
{
    private readonly List<IPool<Bullet>> _pools;

    public BulletPoolContainer(int size = 10)
    {
        _pools = new List<IPool<Bullet>>(size);

        for (int i = 0; i < size; i++)
            _pools.Add(new BulletPool());
    }

    public IPool<Bullet> this[int index]
    {
        get { return _pools[index]; }
    }
}