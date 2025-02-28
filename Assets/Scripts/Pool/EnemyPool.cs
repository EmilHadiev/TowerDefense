using System.Collections.Generic;

public class EnemyPool : IPool<Enemy>
{
    private List<Enemy> _enemies;

    public EnemyPool(int size = 15)
    {
        _enemies = new List<Enemy>(size);
    }

    public void Add(Enemy item) => _enemies.Add(item);

    public bool TryGet(out Enemy enemy)
    {
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