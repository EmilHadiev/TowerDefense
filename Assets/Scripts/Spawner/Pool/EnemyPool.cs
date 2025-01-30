using System.Collections.Generic;

public class EnemyPool : IPool<Enemy>
{
    private List<Enemy> _enemies;

    public EnemyPool()
    {
        _enemies = new List<Enemy>(15);
    }

    public void Add(Enemy item) => _enemies.Add(item);

    public bool TryGet(out Enemy item)
    {
        for (int i = 0; i < _enemies.Count; i++)
        {
            if (_enemies[i].gameObject.activeInHierarchy == false)
            {
                item = _enemies[i];
                return true;
            }
        }

        item = null;
        return false;
    }
}