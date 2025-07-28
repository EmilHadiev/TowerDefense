using System;
using UnityEngine;
using Zenject;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private int _size;
    [field: SerializeField] public EnemyType EnemyType { get; private set; }

    private IEnemyFactory _factory;
    private IFPSLimiter _fpsLimiter;
    private IPool<Enemy> _pool;

    [Inject]
    private void Constructor(IEnemyFactory enemyFactory, IFPSLimiter fpsLimiter)
    {
        _factory = enemyFactory;
        _fpsLimiter = fpsLimiter;
    }

    private void Start()
    {
        _pool = new EnemyPool(_fpsLimiter);

        CreateEnemies();
    }

    private void CreateEnemies()
    {
        for (int i = 0; i < _size; i++)
            CreateEnemy();
    }

    private void CreateEnemy()
    {
        Enemy enemy = _factory.Create(EnemyType);
        _pool.Add(enemy);
        enemy.gameObject.SetActive(false);
        SetPosition(enemy);
    }

    public bool TrySpawn(Vector3 position = default, Quaternion rotation = default)
    {
        if (_pool.TryGet(out Enemy enemy))
        {
            enemy.gameObject.SetActive(true);

            if (position == default && rotation == default)
                SetPosition(enemy);
            else
                SetPosition(enemy, position, rotation);
            
            return true;
        }

        return false;
    }

    private void SetPosition(Enemy enemy)
    {
        enemy.transform.position = transform.position;
        enemy.transform.rotation = transform.rotation;
    }

    private void SetPosition(Enemy enemy, Vector3 position, Quaternion rotation)
    {
        enemy.transform.position = position;
        enemy.transform.rotation = rotation;
    }
}