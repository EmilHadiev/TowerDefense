using System.Collections;
using UnityEngine;
using Zenject;

public class MageAbility : MonoBehaviour
{
    private const int WaitingTime = 2;
    private const int MaxEnemies = 2;

    private EnemySpawnPosition[] _spawnPositions = new EnemySpawnPosition[MaxEnemies];
    private EnemySpawnerAbility[] _spawners;
    private EnemySpawnerAbilityView[] _views;

    private Coroutine _spawnCoroutine;
    private WaitForSeconds _delay;

    private IInstantiator _instantiator;

    private void Awake()
    {
        _delay = new WaitForSeconds(WaitingTime);
        _spawnPositions = GetComponentsInChildren<EnemySpawnPosition>();
    }

    private void OnEnable()
    {
        StopSpawn();

        _spawnCoroutine = StartCoroutine(SpawnCoroutine());
    }

    private void OnDisable() => StopSpawn();

    private void Start()
    {
        _spawners = new EnemySpawnerAbility[MaxEnemies];
        _views = new EnemySpawnerAbilityView[MaxEnemies];

        for (int i = 0; i < MaxEnemies; i++)
        {
            _spawners[i] = new EnemySpawnerAbility(_spawnPositions[i], _instantiator);
            _views[i] = new EnemySpawnerAbilityView(_spawnPositions[i], _instantiator);
        }
    }

    private void StopSpawn()
    {
        StopSpawnView();

        if (_spawnCoroutine != null)
            StopCoroutine(_spawnCoroutine);
    }

    [Inject]
    private void Constructor(IInstantiator instantiator) => _instantiator = instantiator;

    private IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            yield return _delay;
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        for (int i = 0; i < MaxEnemies; i++)
        {
            if (_spawners[i].TrySpawn())
                _views[i].Show();
        }
    }

    private void StopSpawnView()
    {
        if (_views == null)
            return;

        foreach (var view in _views)
            view.Stop();
    }
}