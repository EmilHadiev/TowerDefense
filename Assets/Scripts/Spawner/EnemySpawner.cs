using UnityEngine;
using Zenject;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private int _size;
    [SerializeField] private EnemyType _enemyType;

    private IInstantiator _instantiator;
    private IFPSLimiter _fpsLimiter;
    private IEnemyFactory _factory;
    private IPool<Enemy> _pool;

    [Inject]
    private void Constructor(IInstantiator instantiator, IFPSLimiter fpsLimiter)
    {
        _instantiator = instantiator;
        _fpsLimiter = fpsLimiter;
    }

    private void Start()
    {
        _factory = new EnemyFactory(_instantiator);
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
        Enemy enemy = _factory.Get(_enemyType);
        _pool.Add(enemy);
        enemy.gameObject.SetActive(false);
        SetPosition(enemy);
    }

    public bool TrySpawn()
    {
        if (_pool.TryGet(out Enemy enemy))
        {
            enemy.gameObject.SetActive(true);
            SetPosition(enemy);
            return true;
        }

        return false;
    }

    private void SetPosition(Enemy enemy)
    {
        enemy.transform.position = transform.position;
        enemy.transform.rotation = transform.rotation;
    }
}