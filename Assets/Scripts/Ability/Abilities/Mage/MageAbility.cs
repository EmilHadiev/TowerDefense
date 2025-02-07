using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class MageAbility : MonoBehaviour
{
    private const int WaitingTime = 1;
    private const int MaxEnemies = 2;

    private EnemySpawnPosition[] _spawnPosition = new EnemySpawnPosition[MaxEnemies];

    private Coroutine _spawnCoroutine;
    private WaitForSeconds _delay;

    private EnemyFactory _factory;
    private IInstantiator _instantiator;

    private void Awake()
    {
        _delay = new WaitForSeconds(WaitingTime);
        _spawnPosition = GetComponentsInChildren<EnemySpawnPosition>();
    }

    private void OnEnable()
    {
        StopSpawn();

        _spawnCoroutine = StartCoroutine(SpawnCoroutine());
    }

    private void OnDisable() => StopSpawn();

    private void Start() => _factory = new EnemyFactory(_instantiator);

    private void StopSpawn()
    {
        if (_spawnCoroutine != null)
            StopCoroutine(_spawnCoroutine);
    }

    [Inject]
    private void Constructor(IInstantiator instantiator)
    {
        _instantiator = instantiator;
        Debug.Log(_instantiator == null);
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
        Enemy enemy = _factory.Get((EnemyType)UnityEngine.Random.Range(0, Enum.GetValues(typeof(EnemyType)).Length));
        enemy.transform.position = _spawnPosition[0].transform.position;
        enemy.transform.rotation = _spawnPosition[0].transform.rotation;
    }
}