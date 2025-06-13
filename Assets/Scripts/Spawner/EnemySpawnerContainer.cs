using System.Collections;
using UnityEngine;
using Zenject;

public class EnemySpawnerContainer : MonoBehaviour,  ILevelState
{
    [SerializeField] private EnemySpawner[] _spawners;

    private readonly WaitForSeconds _delay = new WaitForSeconds(Constants.EnemySpawnDelay);

    private EnemyCounter _counter;
    private ILevelStateSwitcher _switcher;
    private ITrainingMode _trainingMode;
    private int _index = 0;

    private Coroutine _spawnCoroutine;

    private void OnEnable()
    {
        _counter.CapacityReached += OnFilled;
        _counter.AllEnemiesDead += OnEnemyDied;
    }

    private void OnDisable()
    {
        _counter.CapacityReached -= OnFilled;
        _counter.AllEnemiesDead -= OnEnemyDied;
    }

    [Inject]
    private void Constructor(EnemyCounter counter, ILevelStateSwitcher switcher, ITrainingMode trainingMode)
    {
        _counter = counter;
        _switcher = switcher;
        _trainingMode = trainingMode;

        if (switcher == null)
            Debug.LogError("IS NULL!");
    }

    public void Enter()
    {
        _counter.Reset();
        Exit();
        _spawnCoroutine = StartCoroutine(SpawnEnemyCoroutine());
    }

    public void Exit()
    {
        if (_spawnCoroutine != null)
            StopCoroutine(_spawnCoroutine);
    }

    private IEnumerator SpawnEnemyCoroutine()
    {
        while (true)
        {
            yield return _delay;
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        if (_index >= _spawners.Length)
            _index = 0;

        if (_spawners[_index].TrySpawn())
        {
            _index++;
            _counter.Add();
        }
    }

    private void OnFilled() => Exit();

    private void OnEnemyDied()
    {
        if (_trainingMode.IsTrainingProcess())
        {
            _trainingMode.ShowNextTraining();
            Debug.Log("ѕќка что тренировочный процесс");
            return;
        }
        else
            _switcher.SwitchState<WaitingLevelState>();
    }
}