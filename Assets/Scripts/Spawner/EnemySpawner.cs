using UnityEngine;
using Zenject;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private int _size;
    [SerializeField] private EnemyType _enemyType;

    private IInstantiator _instantiator;
    private FPSCounter _fpsCounter;
    private IEnemyFactory _factory;
    private IPool<Enemy> _pool;

    [Inject]
    private void Constructor(IInstantiator instantiator, FPSCounter fPSCounter)
    {
        _instantiator = instantiator;
        _fpsCounter = fPSCounter;
    }

    private void Start()
    {
        _factory = new EnemyFactory(_instantiator);
        _pool = new EnemyPool(_fpsCounter);

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

    public void SpawnEnemy()
    {
        if (_pool.TryGet(out Enemy enemy))
        {
            enemy.gameObject.SetActive(true);
            SetPosition(enemy);
        }
    }

    private void SetPosition(Enemy enemy)
    {
        enemy.transform.position = transform.position;
        enemy.transform.rotation = transform.rotation;
    }
}