using System.Collections.Generic;

public class EnemyPool : IPool<Enemy>
{
    private readonly List<Enemy> _enemies;
    private readonly Optimizator _counter;

    public EnemyPool(Optimizator counter, int size = 15)
    {
        _enemies = new List<Enemy>(size);
        _counter = counter;
    }

    public void Add(Enemy item) => _enemies.Add(item);

    public bool TryGet(out Enemy enemy)
    {
        if (_counter.IsEnoughFPS == false)
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