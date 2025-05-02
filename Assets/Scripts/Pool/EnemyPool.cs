using System.Collections.Generic;

public class EnemyPool : IPool<Enemy>
{
    private readonly List<Enemy> _enemies;
    private readonly IFPSLimiter _fpsLimiter;

    public EnemyPool(IFPSLimiter fpsLimiter, int size = 15)
    {
        _enemies = new List<Enemy>(size);
        _fpsLimiter = fpsLimiter;
    }

    public void Add(Enemy item) => _enemies.Add(item);

    public bool TryGet(out Enemy enemy)
    {
        if (_fpsLimiter.IsEnoughFPS == false)
        {
            enemy = null;
            return false;
        }

        for (int i = 0; i < _enemies.Count; i++)
        {
            if (_enemies[i].gameObject.activeInHierarchy == false)
            {
                enemy = _enemies[i];
                return true;
            }
        }

        enemy = null;
        return false;
    }
}