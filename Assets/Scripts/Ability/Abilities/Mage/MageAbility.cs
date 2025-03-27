using System.Collections;
using UnityEngine;
using Zenject;

public class MageAbility : MonoBehaviour
{
    private const int WaitingTime = 2;
    private const int MaxEnemies = 2;

    private EnemySpawnPosition[] _spawnPositions = new EnemySpawnPosition[MaxEnemies];
    private EnemySpawnerAbility _spawners;
    private EnemySpawnerAbilityView[] _views;

    private Coroutine _spawnCoroutine;
    private WaitForSeconds _delay;
    private Optimizator _fpsCounter;
    private IInstantiator _instantiator;

    private void Awake()
    {
        _delay = new WaitForSeconds(WaitingTime);
        _spawnPositions = GetComponentsInChildren<EnemySpawnPosition>();

        InitSpawners();
        InitViews();
    }

    private void OnEnable()
    {
        StopSpawn();

        _spawnCoroutine = StartCoroutine(SpawnCoroutine());
    }

    private void OnDisable() => StopSpawn();

    private void InitSpawners()
    {
        _spawners = new EnemySpawnerAbility(_spawnPositions, _instantiator, MaxEnemies, _fpsCounter);
        _spawners.CreateEnemy();
    }

    private void InitViews()
    {
        _views = new EnemySpawnerAbilityView[MaxEnemies];

        for (int i = 0; i < MaxEnemies; i++)
            _views[i] = new EnemySpawnerAbilityView(_spawnPositions[i], _instantiator);
    }

    private void StopSpawn()
    {
        StopSpawnView();

        if (_spawnCoroutine != null)
            StopCoroutine(_spawnCoroutine);
    }

    [Inject]
    private void Constructor(IInstantiator instantiator, Optimizator fPSCounter)
    {
        _fpsCounter = fPSCounter;
        _instantiator = instantiator;
    }

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
            if (_spawners.TrySpawn())
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